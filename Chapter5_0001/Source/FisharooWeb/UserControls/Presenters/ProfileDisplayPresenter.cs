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
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    public class ProfileDisplayPresenter
    {
        private IProfileDisplay _view;
        private IRedirector _redirector;
        private IFriendRepository _friendRepository;
        private IUserSession _userSession;

        public ProfileDisplayPresenter()
        {
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IProfileDisplay view)
        {
            _view = view;
        }

        public void SendFriendRequest(Int32 AccountIdToInvite)
        {
            _redirector.GoToFriendsInviteFriends(AccountIdToInvite);
        }

        public void DeleteFriend(Int32 FriendID)
        {
            if (_userSession.CurrentUser != null)
            {
                _friendRepository.DeleteFriendByID(_userSession.CurrentUser.AccountID, FriendID);
                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
            }
        }
    }
}
