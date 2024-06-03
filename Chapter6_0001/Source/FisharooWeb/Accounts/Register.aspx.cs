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
using Fisharoo.FisharooWeb.Accounts.Presenter;

namespace Fisharoo.FisharooWeb.Accounts
{
    public partial class Register : System.Web.UI.Page, IRegister
    {
        private RegisterPresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new RegisterPresenter();
            _presenter.Init(this);
        }

        protected void wizRegister_ActiveStepChanged(object sender, EventArgs e)
        {
            if(wizRegister.ActiveStepIndex == 1)
            {
                ViewState.Add("password",txtPassword.Text);
            }
        }

        protected void wizRegister_FinishButtonClicked(object sender, EventArgs e)
        {
            _presenter.Register(txtUsername.Text,ViewState["password"].ToString(),
                txtFirstName.Text,txtLastName.Text,txtEmail.Text,
                txtZipcode.Text,Convert.ToDateTime(txtBirthday.Text), 
                txtCaptcha.Text, chkAgreeWithTerms.Checked, Convert.ToInt32(lblTermID.Text));
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            _presenter.LoginLinkClicked();
        }

        protected void wizRegister_NextButtonClick(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
        }

        public void ShowErrorMessage(string Message)
        {
            lblErrorMessage.Text = Message;
        }

        public void ToggleWizardIndex(int index)
        {
            wizRegister.ActiveStepIndex = index;
        }

        public void ShowAccountCreatedPanel()
        {
            pnlAccountCreated.Visible = true;
            pnlCreateAccount.Visible = false;
        }

        public void ShowCreateAccountPanel()
        {
            pnlAccountCreated.Visible = false;
            pnlCreateAccount.Visible = true;
        }

        public void LoadEmailAddressFromFriendInvitation(string Email)
        {
            txtEmail.Text = Email;
        }

        public void LoadTerms(Term term)
        {
            if (term != null)
            {
                lblTermID.Text = term.TermID.ToString();
                txtTerms.Text = term.Terms;
            }
        }
    }
}
