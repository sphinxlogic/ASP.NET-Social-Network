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
using Fisharoo.FisharooWeb.Groups.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Groups.Presenter
{
    public class ManageGroupPresenter
    {
        private IManageGroup _view;
        private IRedirector _redirector;
        private IWebContext _webContext;
        private IGroupRepository _groupRepository;
        private IGroupService _groupService;
        private IFileService _fileService;
        private IBoardForumRepository _forumRepository;
        private IFileRepository _fileRepository;
        private IGroupToGroupTypeRepository _groupToGroupTypeRepository;
        private IGroupTypeRepository _groupTypeRepository;

        public ManageGroupPresenter()
        {
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _groupRepository = ObjectFactory.GetInstance<IGroupRepository>();
            _groupService = ObjectFactory.GetInstance<IGroupService>();
            _fileService = ObjectFactory.GetInstance<IFileService>();
            _forumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _fileRepository = ObjectFactory.GetInstance<IFileRepository>();
            _groupToGroupTypeRepository = ObjectFactory.GetInstance<IGroupToGroupTypeRepository>();
            _groupTypeRepository = ObjectFactory.GetInstance<IGroupTypeRepository>();
        }

        public void Init(IManageGroup view, bool IsPostBack)
        {
            _view = view;

            //security check for group
            if (_webContext.GroupID > 0 && !_groupService.IsOwnerOrAdministrator(_webContext.CurrentUser.AccountID, _webContext.GroupID))
                _redirector.GoToAccountAccessDenied();

            if(!IsPostBack)
                view.LoadGroupTypes(_groupTypeRepository.GetAllGroupTypes());

            if (_webContext.GroupID > 0 && !IsPostBack)
                view.LoadGroup(_groupRepository.GetGroupByID(_webContext.GroupID),
                               _groupTypeRepository.GetGroupTypesByGroupID(_webContext.GroupID));
        }

        public void SaveGroup(Group group, HttpPostedFile file, List<long> selectedGroupTypeIDs)
        {
            if (group.Description.Length > 2000)
            {
                _view.ShowMessage("Your description is " + group.Description.Length.ToString() +
                                  " characters long and can only be 2000 characters!");
            }
            else
            {
                group.AccountID = _webContext.CurrentUser.AccountID;
                group.PageName = group.PageName.Replace(" ", "-");
                //if this is a new group then check to see if the page name is in use
                if (group.GroupID == 0 && _groupRepository.CheckIfGroupPageNameExists(group.PageName))
                {
                    _view.ShowMessage("The page name you specified is already in use!");
                }
                else
                {
                    if (file.ContentLength > 0)
                    {
                        List<Int64> fileIDs = _fileService.UploadPhotos(1, _webContext.CurrentUser.AccountID,
                                                                        _webContext.Files, 1);
                        //should only be one item uploaded!
                        if (fileIDs.Count == 1)
                            group.FileID = fileIDs[0];
                    }
                    group.GroupID = _groupService.SaveGroup(group);
                    _groupToGroupTypeRepository.SaveGroupTypesForGroup(selectedGroupTypeIDs,group.GroupID);
                    _redirector.GoToGroupsViewGroup(group.PageName);
                }
            }
        }

        public string GetImagePathByID(Int64 ImageID)
        {
            return _fileService.GetFullFilePathByFileID(ImageID, File.Sizes.S);
        }
    }
}
