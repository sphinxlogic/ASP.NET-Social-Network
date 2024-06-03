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
using Fisharoo.FisharooWeb.Friends.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Friends.Presenter
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IFriendRepository _friendRepository;
        private IUserSession _userSession;

        public DefaultPresenter()
        {
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IDefault view)
        {
            _view = view;
            LoadDisplay();
        }

        public void LoadDisplay()
        {
            _view.LoadDisplay(_friendRepository.GetFriendsAccountsByAccountID(_userSession.CurrentUser.AccountID));
        }
    }
}
