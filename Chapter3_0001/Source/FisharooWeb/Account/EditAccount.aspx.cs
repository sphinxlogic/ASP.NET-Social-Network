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
using Fisharoo.FisharooWeb.Account.Interface;
using Fisharoo.FisharooWeb.Account.Presenter;

namespace Fisharoo.FisharooWeb.Account
{
    public partial class EditAccount : System.Web.UI.Page, IEditAccount
    {
        private EditAccountPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new EditAccountPresenter();
            _presenter.Init(this, IsPostBack);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.UpdateAccount(txtOldPassword.Text,txtNewPassword.Text,lblUsername.Text, txtFirstName.Text,
                txtLastName.Text, txtEmail.Text, txtZipCode.Text,Convert.ToDateTime(txtBirthDate.Text));
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text = Message;
        }

        public void LoadCurrentInformation(FisharooCore.Core.Domain.Account account)
        {
            txtBirthDate.Text = String.Format("{0:d}",account.BirthDate);
            txtEmail.Text = account.Email;
            txtFirstName.Text = account.FirstName;
            txtLastName.Text = account.LastName;
            txtZipCode.Text = account.Zip;
            lblUsername.Text = account.Username;
        }
    }
}
