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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Mail.Interface;
using Fisharoo.FisharooWeb.Mail.Presenter;

namespace Fisharoo.FisharooWeb.Mail
{
    public partial class NewMessage : System.Web.UI.Page, INewMessage 
    {
        private NewMessagePresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new NewMessagePresenter();
            _presenter.Init(this);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string[] to = txtTo.Text.Split(new char[] {',', ';'});
            _presenter.SendMessage(txtSubject.Text,txtMessage.Text,to);

            pnlSendMessage.Visible = false;
            pnlSent.Visible = true;
        }

        public void LoadReply(MessageWithRecipient message)
        {
            txtSubject.Text = "RE: " + message.Message.Subject;
            txtTo.Text = message.Sender.Username;
            txtMessage.Text = "<BR><BR><HR>Sent On: " + message.Message.CreateDate.ToString() + "<BR>Subject: " + message.Message.Subject + "<BR>Message: " + message.Message.Body;
        }

        public void LoadTo(string Username)
        {
            txtTo.Text = Username;
        }
    }
}
