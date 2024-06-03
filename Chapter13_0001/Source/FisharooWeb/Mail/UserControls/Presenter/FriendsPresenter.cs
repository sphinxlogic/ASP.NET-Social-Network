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
using Fisharoo.FisharooWeb.Mail.UserControls.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Mail.UserControls.Presenter
{
    public class FriendsPresenter
    {
        private IFriends _view;
        private IFriendRepository _friendRepository;
        private IUserSession _userSession;
        public FriendsPresenter()
        {
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IFriends view)
        {
            _view = view;
            _view.LoadFriends(_friendRepository.GetFriendsAccountsByAccountID(_userSession.CurrentUser.AccountID));
        }
    }
}
