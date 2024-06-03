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
using Fisharoo.FisharooWeb.Photos.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos.Presenter
{
    public class EditPhotosPresenter
    {
        private IEditPhotos _view;
        private IFileRepository _fileRepository;
        private IWebContext _webContext;
        public EditPhotosPresenter()
        {
            _fileRepository = ObjectFactory.GetInstance<IFileRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IEditPhotos view)
        {
            _view = view;
            _view.LoadFiles(_fileRepository.GetFilesByFolderID(_webContext.AlbumID));
        }

        public void SaveResults(Dictionary<int,string> fileDescriptions)
        {
            _fileRepository.UpdateDescriptions(fileDescriptions);
        }
    }
}
