using System;
using System.Data;
using System.Configuration;
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
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooWeb.Account.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Account.Presenter
{
    public class EditAccountPresenter
    {
        private IEditAccount _view;
        private IUserSession _userSession;
        private IAccountService _accountService;
        private IAccountRepository _accountRepository;
        private FisharooCore.Core.Domain.Account account;
        private IRedirector _redirector;
        private IEmail _email;

        public EditAccountPresenter()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _accountService = ObjectFactory.GetInstance<IAccountService>();
            _email = ObjectFactory.GetInstance<IEmail>();
        }

        public void Init(IEditAccount View, bool IsPostBack)
        {
            _view = View;

            if (_userSession.CurrentUser != null)
                account = _userSession.CurrentUser;
            else 
                _redirector.GoToAccountLoginPage();

            if(!IsPostBack)
                LoadCurrentUser();
        }

        private void LoadCurrentUser()
        {
            _view.LoadCurrentInformation(_userSession.CurrentUser);
        }

        public void UpdateAccount(string OldPassword, string NewPassword, string Username,
            string FirstName, string LastName, string Email, 
            string ZipCode, DateTime BirthDate)
        {
            //verify that this user is the same as the logged in user
            if(Cryptography.Encrypt(OldPassword,Username) == account.Password)
            {
                if (Email != _userSession.CurrentUser.Email)
                {
                    if (!_accountService.EmailInUse(Email))
                    {
                        account.Email = Email;
                        account.EmailVerified = false;
                        _email.SendEmailAddressVerificationEmail(account.Username, Email);                        
                    }
                    else
                    {
                        _view.ShowMessage("The email your entered is already in our system!");
                        return;
                    }
                }

                if(!string.IsNullOrEmpty(NewPassword))
                    account.Password = Cryptography.Encrypt(NewPassword, Username);

                account.FirstName = FirstName;
                account.LastName = LastName;

                account.Zip = ZipCode;
                account.BirthDate = BirthDate;

                _accountRepository.SaveAccount(account);
                _view.ShowMessage("Your account has been updated!");
            }
            else
            {
                _view.ShowMessage("The password you entered doesn't match your current password!  Please try again.");
            }
        }
    }
}
