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
    public partial class Login : System.Web.UI.Page, ILogin
    {
        private LoginPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new LoginPresenter();
            _presenter.Init(this);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            _presenter.Login(txtUsername.Text, txtPassword.Text);
        }

        protected void lbRecoverPassword_Click(object sender, EventArgs e)
        {
            _presenter.GoToRecoverPassword();
        }

        protected void lbRegister_Click(object sender, EventArgs e)
        {
            _presenter.GoToRegister();
        }

        public void DisplayMessage(string Message)
        {
            lblMessage.Text = Message;
        }
    }
}
