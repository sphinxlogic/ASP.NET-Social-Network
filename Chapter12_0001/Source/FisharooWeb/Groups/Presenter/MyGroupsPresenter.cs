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
    public class MyGroupsPresenter
    {
        private IMyGroups _view;
        private IRedirector _redirector;
        private IWebContext _webContext;
        private IGroupRepository _groupRepository;
        private IFileService _fileService;
        private IBoardForumRepository _boardForumRepository;
        private IBoardPostRepository _boardPostRepository;
        private IGroupForumRepository _groupForumRepository;
        private IGroupMemberRepository _groupMemberRepository;

        public MyGroupsPresenter()
        {
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _groupRepository = ObjectFactory.GetInstance<IGroupRepository>();
            _fileService = ObjectFactory.GetInstance<IFileService>();
            _boardPostRepository = ObjectFactory.GetInstance<IBoardPostRepository>();
            _boardForumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _groupForumRepository = ObjectFactory.GetInstance<IGroupForumRepository>();
            _groupMemberRepository = ObjectFactory.GetInstance<IGroupMemberRepository>();
        }

        public void Init(IMyGroups view)
        {
            _view = view;
            LoadData();
        }

        public void LoadData()
        {
            _view.LoadData(_groupRepository.GetGroupsOwnedByAccount(_webContext.CurrentUser.AccountID));
        }

        public string GetImageByID(Int64 ImageID, File.Sizes Size)
        {
            return _fileService.GetFullFilePathByFileID(ImageID, Size);
        }

        public void GoToGroup(string GroupPageName)
        {
            _redirector.GoToGroupsViewGroup(GroupPageName);
        }

        public void DeleteGroup(int GroupID)
        {
            BoardForum forum = _boardForumRepository.GetForumByGroupID(GroupID);
            if (forum != null)
            {
                _boardPostRepository.DeletePostsByForumID(forum.ForumID);
                _groupForumRepository.DeleteGroupForum(forum.ForumID, GroupID);
                _boardForumRepository.DeleteForum(forum);
            }
            _groupMemberRepository.DeleteAllGroupMembersForGroup(GroupID);
            _groupRepository.DeleteGroup(GroupID);
            LoadData();
        }

        public void EditGroup(int GroupID)
        {
            _redirector.GoToGroupsManageGroup(GroupID);
        }
    }
}
