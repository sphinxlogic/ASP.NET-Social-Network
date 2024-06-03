using System;
using System.Collections.Generic;
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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooWeb.Accounts.Presenter
{
    public class RegisterPresenter
    {
        private IRegister _view;
        private IAccountRepository _accountRepository;
        private IPermissionRepository _permissionRepository;
        private ITermRepository _termRepository;
        private IAccountService _accountService;
        private IWebContext _webContext;
        private IEmail _email;
        private IFriendInvitationRepository _friendInvitationRepository;
        private IFriendService _friendService;
        private FriendInvitation friendInvitation;

        public void Init(IRegister View)
        {
            _view = View;
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _permissionRepository = ObjectFactory.GetInstance<IPermissionRepository>();
            _termRepository = ObjectFactory.GetInstance<ITermRepository>();
            _accountService = ObjectFactory.GetInstance<IAccountService>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _email = ObjectFactory.GetInstance<IEmail>();
            _friendInvitationRepository = ObjectFactory.GetInstance<IFriendInvitationRepository>();
            _friendService = ObjectFactory.GetInstance<IFriendService>();

            _view.LoadTerms(_termRepository.GetCurrentTerm());
            if (!string.IsNullOrEmpty(_webContext.FriendshipRequest))
            {
                friendInvitation = _friendInvitationRepository.GetFriendInvitationByGUID(new Guid(_webContext.FriendshipRequest));
                _view.LoadEmailAddressFromFriendInvitation(friendInvitation.Email);
            }
        }

        public void LoginLinkClicked()
        {
            IRedirector redirector = ObjectFactory.GetInstance<IRedirector>();
            redirector.GoToAccountLoginPage();
        }

        public void Register(string Username, string Password, 
            string FirstName, string LastName, string Email, 
            string Zip, DateTime BirthDate, string Captcha, bool AgreesWithTerms, Int32 TermID)
        {
            if (AgreesWithTerms)
            {
                if (Captcha == _webContext.CaptchaImageText)
                {
                    Account a =
                        new Account();
                    a.FirstName = FirstName;
                    a.LastName = LastName;
                    a.Email = Email;
                    a.BirthDate = BirthDate;
                    a.Zip = Zip;
                    a.Username = Username;
                    a.Password = Password.Encrypt(Username);
                    a.TermID = TermID;

                    if (_accountService.EmailInUse(Email))
                    {
                        _view.ShowErrorMessage("This email is already in use!");
                        _view.ToggleWizardIndex(0);
                    }
                    else if (_accountService.UsernameInUse(Username))
                    {
                        _view.ShowErrorMessage("This username is already in use!");
                        _view.ToggleWizardIndex(0);
                    }
                    else
                    {
                        _accountRepository.SaveAccount(a);

                        Permission publicPermission = _permissionRepository.GetPermissionByName("PUBLIC");
                        Permission registeredPermission = _permissionRepository.GetPermissionByName("REGISTERED");
                        Account newAccount = _accountRepository.GetAccountByEmail(Email);

                        _accountRepository.AddPermission(newAccount, publicPermission);
                        _accountRepository.AddPermission(newAccount, registeredPermission);
                        
                        //if this registration came via a friend request...
                        if(friendInvitation != null)
                        {
                            _friendService.CreateFriendFromFriendInvitation(new Guid(_webContext.FriendshipRequest),newAccount);
                        }

                        _email.SendEmailAddressVerificationEmail(a.Username,a.Email);
                        _view.ShowAccountCreatedPanel();
                    }
                }
                else
                {
                    _view.ShowErrorMessage("Your entry doesn't match the CAPTCHA image.  Please try again.");
                }
            }
            else
            {
                _view.ToggleWizardIndex(2);
                _view.ShowErrorMessage("You can't create an account on this site if you don't agree with our terms!");
            }
        }
    }
}
