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
using Fisharoo.FisharooWeb.Accounts.Interface;
using Fisharoo.FisharooWeb.Accounts.Presenter;

namespace Fisharoo.FisharooWeb.Accounts
{
    public partial class VerifyEmail : System.Web.UI.Page, IVerifyEmail
    {
        private VerifyEmailPresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new VerifyEmailPresenter();
            _presenter.Init(this);
        }

        public void ShowMessage(string Message)
        {
            lblMsg.Text = Message;
        }
    }
}
