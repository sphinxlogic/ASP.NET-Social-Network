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
using Fisharoo.FisharooWeb.Groups.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Groups.Presenter
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IRedirector _redirector;
        private IWebContext _webContext;
        private IGroupRepository _groupRepository;
        private IFileService _fileService;

        public DefaultPresenter()
        {
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _groupRepository = ObjectFactory.GetInstance<IGroupRepository>();
            _fileService = ObjectFactory.GetInstance<IFileService>();
        }

        public void Init(IDefault view)
        {
            _view = view;
            _view.LoadData(_groupRepository.GetLatestGroups());
        }

        public string GetImageByID(Int64 ImageID, File.Sizes Size)
        {
            return _fileService.GetFullFilePathByFileID(ImageID, Size);
        }

        public void GoToGroup(string GroupPageName)
        {
            _redirector.GoToGroupsViewGroup(GroupPageName);
        }
    }
}
