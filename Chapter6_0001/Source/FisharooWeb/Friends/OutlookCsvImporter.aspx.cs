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
using Fisharoo.FisharooWeb.Friends.Interface;
using Fisharoo.FisharooWeb.Friends.Presenter;

namespace Fisharoo.FisharooWeb.Friends
{
    public partial class OutlookCsvImporter : System.Web.UI.Page, IOutlookCsvImporter 
    {
        private OutlookCsvImporterPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new OutlookCsvImporterPresenter();
            _presenter.Init(this);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = fuContacts.PostedFile;
            _presenter.ParseEmails(file);
        }

        public void ShowParsedEmail(List<string> Emails)
        {
            pnlUpload.Visible = false;
            pnlResult.Visible = false;
            pnlEmails.Visible = true;
            cblEmails.DataSource = Emails;
            cblEmails.DataBind();
        }

        protected void btnInviteContacts_Click(object sender, EventArgs e)
        {
            string emails = "";
            foreach (ListItem li in cblEmails.Items)
            {
                if(li != null && li.Selected)
                    emails += li.Text + ",";
            }
            emails = emails.Substring(0, emails.Length - 1);
            _presenter.InviteContacts(emails);
        }

        public void ShowInvitationResult(string Message)
        {
            lblMessage.Text = Message;
            pnlUpload.Visible = false;
            pnlEmails.Visible = false;
            pnlResult.Visible = true;
        }
    }
}
