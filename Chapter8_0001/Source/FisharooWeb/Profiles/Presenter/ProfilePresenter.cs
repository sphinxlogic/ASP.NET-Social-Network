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
using Fisharoo.FisharooWeb.Profiles.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles.Presenter
{
    public class ProfilePresenter
    {
        private IProfile _view;
        private IWebContext _webContext;
        private IUserSession _userSession;
        private IAccountRepository _accountRepository;
        private IAccountService _accountService;
        private IRedirector _redirector;
        private IPrivacyRepository _privacyRepository;
        private IPrivacyService _privacyService;
        private IFriendRepository _friendRepository;
        private IStatusUpdateRepository _statusUpdateRepository;

        private Account _accountBeingViewed;
        private Account _account;
        private List<PrivacyFlag> _privacyFlags;

        public ProfilePresenter()
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _accountService = ObjectFactory.GetInstance<IAccountService>();
            _privacyRepository = ObjectFactory.GetInstance<IPrivacyRepository>();
            _privacyService = ObjectFactory.GetInstance<IPrivacyService>();
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _statusUpdateRepository = ObjectFactory.GetInstance<IStatusUpdateRepository>();

            _account = _userSession.CurrentUser;
            _redirector = ObjectFactory.GetInstance<IRedirector>();

            if (_webContext.AccountID > 0)
                _accountBeingViewed = _accountService.GetAccountByID(_webContext.AccountID);
            else
                _accountBeingViewed = _userSession.CurrentUser;

            if(_accountBeingViewed == null)
                _redirector.GoToAccountLoginPage();

            _privacyFlags = _privacyRepository.GetPrivacyFlagsByProfileID(_accountBeingViewed.Profile.ProfileID);
        }

        public void Init(IProfile View)
        {
            _view = View;
            _view.SetAvatar(_accountBeingViewed.AccountID);
            _view.DisplayInfo(_accountBeingViewed);
            _view.LoadFriends(_friendRepository.GetFriendsAccountsByAccountID(_accountBeingViewed.AccountID));
            _view.LoadStatusUpdates(_statusUpdateRepository.GetTopNStatusUpdatesByAccountID(_accountBeingViewed.AccountID,5));
            TogglePrivacy();
        }

        public bool IsAttributeVisible(Int32 PrivacyFlagTypeID)
        {
            return _privacyService.ShouldShow(PrivacyFlagTypeID, _accountBeingViewed, _account, _privacyFlags);
        }

        private void TogglePrivacy()
        {
            _view.pnlPrivacyIMVisible(_privacyService.ShouldShow((int)PrivacyFlagType.PrivacyFlagTypes.IM,_accountBeingViewed, _account, _privacyFlags));
            _view.pnlPrivacyAccountInfoVisible(_privacyService.ShouldShow((int)PrivacyFlagType.PrivacyFlagTypes.AccountInfo,_accountBeingViewed, _account, _privacyFlags));
            _view.pnlPrivacyTankInfoVisible(_privacyService.ShouldShow((int)PrivacyFlagType.PrivacyFlagTypes.TankInfo,_accountBeingViewed, _account, _privacyFlags));
        }

        public void GoToStatusUpdates()
        {
            _redirector.GoToProfilesStatusUpdates(_accountBeingViewed.AccountID);
        }
    }
}
