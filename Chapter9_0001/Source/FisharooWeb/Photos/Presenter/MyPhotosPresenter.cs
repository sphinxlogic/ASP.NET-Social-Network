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
using Fisharoo.FisharooWeb.Photos.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos.Presenter
{
    public class MyPhotosPresenter
    {
        private IFolderRepository _folderRepository;
        private IFileRepository _fileRepository;
        private IUserSession _userSession;
        private IRedirector _redirector;
        private IMyPhotos _view;
        public MyPhotosPresenter()
        {
            _folderRepository = ObjectFactory.GetInstance<IFolderRepository>();
            _fileRepository = ObjectFactory.GetInstance<IFileRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }
        public void Init(IMyPhotos view)
        {
            _view = view;
            if(!(_userSession.CurrentUser == null))
                _view.LoadUI(_folderRepository.GetFoldersByAccountID(_userSession.CurrentUser.AccountID));
        }
        public void DeleteFolder(Int64 FolderID)
        {
            Folder folder = _folderRepository.GetFolderByID(FolderID);
            _fileRepository.DeleteFilesInFolder(folder);
            _folderRepository.DeleteFolder(folder);
            _redirector.GoToPhotosMyPhotos();
        }
    }
}
