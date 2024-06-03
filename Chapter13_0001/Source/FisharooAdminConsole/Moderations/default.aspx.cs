using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fisharoo.FisharooAdminConsole.Moderations.Interface;
using Fisharoo.FisharooAdminConsole.Moderations.Presenters;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooAdminConsole.Moderations
{
    public partial class _default : System.Web.UI.Page, IDefault
    {
        private DefaultPresenter _presenter;
        private IFileService _fileService;
        protected IConfiguration _configuration;
        
        public _default()
        {
            _fileService = ObjectFactory.GetInstance<IFileService>();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultPresenter();
            _presenter.Init(this, IsPostBack);
        }

        protected void repModeration_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                PlaceHolder phContent = e.Item.FindControl("phContent") as PlaceHolder;
                Moderation moderation = e.Item.DataItem as Moderation;
                string file = _fileService.GetFullFilePathByFileID(moderation.SystemObjectRecordID, File.Sizes.S);

                if(moderation.SystemObjectID == 5)
                {
                    Image img = new Image();
                    img.ImageUrl = _configuration.WebSiteURL + "files/photos/" + file;
                    phContent.Controls.Add(img);
                }

            }
        }

        public void LoadData(List<Moderation> moderations)
        {
            repModeration.DataSource = moderations;
            repModeration.DataBind();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<ModerationResult> results = new List<ModerationResult>();
            foreach (RepeaterItem item in repModeration.Controls)
            {
                if(item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
                {
                    CheckBox chkApprove = item.FindControl("chkApprove") as CheckBox;
                    CheckBox chkDeny = item.FindControl("chkDeny") as CheckBox;
                    Literal litSystemObjectID = item.FindControl("litSystemObjectID") as Literal;
                    Literal litSystemObjectRecordID = item.FindControl("litSystemObjectRecordID") as Literal;
                    TextBox txtGagDate = item.FindControl("txtGagDate") as TextBox;
                    TextBox txtReason = item.FindControl("txtReason") as TextBox;
                    Literal litAccountID = item.FindControl("litAccountID") as Literal;
                    Literal litAccountUsername = item.FindControl("litAccountUsername") as Literal;

                    if(chkDeny.Checked || chkApprove.Checked)
                    {
                        ModerationResult mr = new ModerationResult();
                        mr.SystemObjectID = Convert.ToInt32(litSystemObjectID.Text);
                        mr.SystemObjectRecordID = Convert.ToInt64(litSystemObjectRecordID.Text);

                        if (chkApprove.Checked)
                        {
                            mr.IsApproved = true;
                            results.Add(mr);
                        }

                        //deny wins
                        if (chkDeny.Checked)
                        {
                            mr.IsApproved = false;
                            results.Add(mr);
                        }
                    }

                    if(!string.IsNullOrEmpty(txtGagDate.Text))
                    {
                        _presenter.GagUserUntil(Convert.ToInt32(litAccountID.Text), 
                            litAccountUsername.Text, 
                            DateTime.Parse(txtGagDate.Text), 
                            txtReason.Text);
                    }
                }
            }

            if(results.Count() > 0)
                _presenter.SaveModerationResults(results);
        }

        public void ClearData()
        {
            repModeration.Controls.Clear();
        }
    }
}
