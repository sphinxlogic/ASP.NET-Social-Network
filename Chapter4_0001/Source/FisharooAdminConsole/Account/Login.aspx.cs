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
using Fisharoo.FisharooAdminConsole.Presenter;

namespace Fisharoo.FisharooAdminConsole
{
    public partial class Login : System.Web.UI.Page, Fisharoo.FisharooAdminConsole.Interface.ILogin
    {
        private LoginPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new LoginPresenter();
            _presenter.Init(this);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            _presenter.LogIn(txtUsername.Text, txtPassword.Text);
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text = Message;
        }
    }
}
