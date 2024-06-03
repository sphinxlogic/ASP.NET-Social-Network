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
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using Fisharoo.FisharooWeb.UserControls.Presenters;

namespace Fisharoo.FisharooWeb.UserControls
{
    //CHAPTER 3
    //TODO: replace username with profile user control that links username to profile
    public partial class LogInLinks : System.Web.UI.UserControl, ILogInLinks
    {
        private LogInLinksPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new LogInLinksPresenter();
            _presenter.Init(this);
        }

        protected void lbHome_Click(object sender, EventArgs e)
        {
            _presenter.Home();
        }

        protected void lbLogOut_Click(object sender, EventArgs e)
        {
            _presenter.LogOut();
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            _presenter.LogIn();
        }

        protected void lbRegister_Click(object  sender, EventArgs e)
        {
            _presenter.Register();
        }

        protected void lbEditAccount_Click(object sender, EventArgs e)
        {
            _presenter.EditAccount();
        }

        public void ShowAppropriateLoginStatePanel(bool IsLoggedIn, string Username)
        {
            if (IsLoggedIn)
            {
                pnlLoggedIn.Visible = true;
                pnlNotLoggedIn.Visible = false;
                lblUsername.Text = "Welcome " + Username;
            }
            else
            {
                pnlLoggedIn.Visible = false;
                pnlNotLoggedIn.Visible = true;
            }
        }
    }
}