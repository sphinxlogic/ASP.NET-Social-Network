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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Mail.Interface;
using Fisharoo.FisharooWeb.Mail.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Mail
{
    public partial class ReadMessage : System.Web.UI.Page, IReadMessage
    {
        private ReadMessagePresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ReadMessagePresenter();
            _presenter.Init(this);
        }

        public void LoadMessage(MessageWithRecipient message)
        {
            linkFrom.Text = message.Sender.Username;
            linkFrom.NavigateUrl = "~/" + message.Sender.Username;
            lblSubject.Text = message.Message.Subject;
            lblMessage.Text = message.Message.Body;
        }

        public void btnReply_Click(object sender, EventArgs e)
        {
            _presenter.Reply();
        }
    }
}
