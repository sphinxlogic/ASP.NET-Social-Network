using System;
using System.Collections;
using System.Collections.Generic;
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
using Fisharoo.FisharooWeb.Profiles.Interface;
using Fisharoo.FisharooWeb.Profiles.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class ManagePrivacy : System.Web.UI.Page, IManagePrivacy
    {
        private ManagePrivacyPresenter _presenter;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            _presenter = new ManagePrivacyPresenter();
            _presenter.Init(this);
            
        }

        public void ShowPrivacyTypes(List<PrivacyFlagType> PrivacyFlagTypes, 
            List<VisibilityLevel> VisibilityLevels, 
            List<PrivacyFlag> PrivacyFlags)
        {
            foreach (PrivacyFlagType type in PrivacyFlagTypes)
            {
                //Add the field name to the display
                phPrivacyFlagTypes.Controls.Add(new LiteralControl("<div class=\"divContainerRow\">")); //start container
                phPrivacyFlagTypes.Controls.Add(new LiteralControl("<div class=\"divContainerCellHeader\">")); //start cell header
                phPrivacyFlagTypes.Controls.Add(new LiteralControl(type.FieldName + ":"));
                phPrivacyFlagTypes.Controls.Add(new LiteralControl("</div>")); //end cell header
                phPrivacyFlagTypes.Controls.Add(new LiteralControl("<div class=\"divContainerCell\">")); //start cell

                //Create the visibility drop down
                DropDownList ddlVisibility = new DropDownList();
                ddlVisibility.ID = "ddlVisibility" + type.PrivacyFlagTypeID.ToString();
                foreach (VisibilityLevel level in VisibilityLevels)
                {
                    ListItem li = new ListItem(level.Name,level.VisibilityLevelID.ToString());
                    if(!IsPostBack)
                        li.Selected = _presenter.IsFlagSelected(type.PrivacyFlagTypeID, level.VisibilityLevelID, PrivacyFlags);
                    ddlVisibility.Items.Add(li);
                }
                phPrivacyFlagTypes.Controls.Add(ddlVisibility);

                phPrivacyFlagTypes.Controls.Add(new LiteralControl("</div>")); //end cell
                phPrivacyFlagTypes.Controls.Add(new LiteralControl("</div>")); //end container
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            foreach (PrivacyFlagType type in _presenter.GetPrivacyFlagTypes())
            {
                DropDownList ddlVisibility =
                    phPrivacyFlagTypes.FindControl("ddlVisibility" + type.PrivacyFlagTypeID.ToString()) as DropDownList;
                if(ddlVisibility != null)
                    _presenter.SavePrivacyFlag(type.PrivacyFlagTypeID,Convert.ToInt32(ddlVisibility.SelectedValue));
            }

            lblMessage.Text = "Your privacy settings were saved successfully!";
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text += Message;
        }
    }
}
