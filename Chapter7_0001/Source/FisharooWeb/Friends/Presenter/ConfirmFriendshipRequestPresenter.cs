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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Friends.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Friends.Presenter
{
    public class ConfirmFriendshipRequestPresenter
    {
        private IConfirmFriendshipRequest _view;
        private IWebContext _webContext;
        private IFriendInvitationRepository _friendInvitationRepository;
        private IAccountRepository _accountRepository;
        private IConfiguration _configuration;
        private IRedirector _redirector;

        public ConfirmFriendshipRequestPresenter()
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _friendInvitationRepository = ObjectFactory.GetInstance<IFriendInvitationRepository>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }

        public void Init(IConfirmFriendshipRequest view)
        {
            _view = view;
            if (!string.IsNullOrEmpty(_webContext.FriendshipRequest))
            {
                FriendInvitation friendInvitation =
                    _friendInvitationRepository.GetFriendInvitationByGUID(new Guid(_webContext.FriendshipRequest));
                if(friendInvitation != null)
                {
                    if (_webContext.CurrentUser != null)
                        LoginClick();

                    Account account = _accountRepository.GetAccountByID(friendInvitation.AccountID);
                    _view.ShowConfirmPanel(true);
                    _view.LoadDisplay(_webContext.FriendshipRequest, account.AccountID, account.FirstName, account.LastName, _configuration.SiteName );
                }
                else
                {
                    _view.ShowConfirmPanel(false);
                    _view.ShowMessage("There was an error validating your invitation.");
                }
            }
        }

        public void LoginClick()
        {
            _redirector.GoToAccountLoginPage(_webContext.FriendshipRequest);
        }

        public void RegisterClick()
        {
            _redirector.GoToAccountRegisterPage(_webContext.FriendshipRequest);
        }
    }
}
