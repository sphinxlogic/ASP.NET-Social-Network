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
using Fisharoo.FisharooWeb.Photos.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos.Presenter
{
    public class DefaultPresenter
    {
        private IFolderService _folderService;
        private IUserSession _userSession;
        private IDefault _view;
        public DefaultPresenter()
        {
            _folderService = ObjectFactory.GetInstance<IFolderService>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }
        public void Init(IDefault view)
        {
            _view = view;
            if(!(_userSession.CurrentUser == null))
                _view.LoadUI(_folderService.GetFriendsFolders(_userSession.CurrentUser.AccountID));
        }
    }
}
