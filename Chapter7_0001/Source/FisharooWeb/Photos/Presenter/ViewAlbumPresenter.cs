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
using Fisharoo.FisharooWeb.Photos.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos.Presenter
{
    public class ViewAlbumPresenter
    {
        private IViewAlbum _view;
        private IFileRepository _fileRepository;
        private IFolderRepository _folderRepository;
        private IWebContext _webContext;
        public ViewAlbumPresenter()
        {
            _fileRepository = ObjectFactory.GetInstance<IFileRepository>();
            _folderRepository = ObjectFactory.GetInstance<IFolderRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }
        public void Init(IViewAlbum view)
        {
            _view = view;
            LoadUI();
        }
        private void LoadUI()
        {
            _view.LoadPhotos(_fileRepository.GetFilesByFolderID(_webContext.AlbumID));
            _view.LoadAlbumDetails(_folderRepository.GetFolderByID(_webContext.AlbumID));
        }
    }
}
