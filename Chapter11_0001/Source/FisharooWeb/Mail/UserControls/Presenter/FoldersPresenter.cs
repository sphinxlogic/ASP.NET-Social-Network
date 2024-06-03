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
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooWeb.Mail.UserControls.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Mail.UserControls.Presenter
{
    public class FoldersPresenter
    {
        private IFolders _view;
        private IMessageFolderRepository _messageFolderRepository;
        private IUserSession _userSession;

        public FoldersPresenter()
        {
            _messageFolderRepository = ObjectFactory.GetInstance<IMessageFolderRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IFolders view)
        {
            _view = view;
            _view.LoadFolders(_messageFolderRepository.GetMessageFoldersByAccountID(_userSession.CurrentUser.AccountID));
        }
    }
}
