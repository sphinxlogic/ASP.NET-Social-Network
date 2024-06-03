using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Groups.Interface;
using Fisharoo.FisharooWeb.Groups.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Groups
{
    public partial class ViewGroup : System.Web.UI.Page, IViewGroup
    {
        private ViewGroupPresenter _presenter;
        private IFileService _fileService;
        protected void Page_Load(object sender, EventArgs e)
        {
            _fileService = ObjectFactory.GetInstance<IFileService>();
            _presenter = new ViewGroupPresenter();
            _presenter.Init(this, IsPostBack);
        }

        public void LoadData(Group group)
        {
            ((SiteMaster)Master).Title = group.Name;
            imgGroupLogo.ImageUrl = "/files/photos/" + _fileService.GetFullFilePathByFileID(group.FileID, File.Sizes.S);
            lblCreateDate.Text = group.CreateDate.ToShortDateString();
            lblUpdateDate.Text = group.UpdateDate.ToShortDateString();
            lblDescription.Text = group.Description;
            lblBody.Text = group.Body;
        }

        public void ShowPublic(bool Visible)
        {
            pnlPublic.Visible = Visible;    
        }

        public void ShowPrivate(bool Visible)
        {
            pnlPrivate.Visible = Visible;
            lblPrivateMessage.Visible = !Visible;
        }

        public void ShowRequestMembership(bool Visible)
        {
            lbRequestMembership.Visible = Visible;
        }

        protected void lbForum_Click(object sender, EventArgs e)
        {
            _presenter.GoToForum();
        }

        protected void lbRequestMembership_Click(object sender, EventArgs e)
        {
            _presenter.RequestMembership();
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text = Message;
        }

        protected void lbViewMembers_Click(object sender, EventArgs e)
        {
            _presenter.ViewMembers();
        }
    }
}
