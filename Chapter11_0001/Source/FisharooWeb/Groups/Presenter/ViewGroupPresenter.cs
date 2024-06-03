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
    public class ViewGroupPresenter
    {
        private IWebContext _webContext;
        private IGroupRepository _groupRepository;
        private IViewGroup _view;
        private IRedirector _redirector;
        private IBoardForumRepository _boardForumRepository;
        private IBoardCategoryRepository _boardCategoryRepository;
        private IGroupMemberRepository _groupMemberRepository;
        private IGroupService _groupService;
        public ViewGroupPresenter()
        {
            _groupRepository = ObjectFactory.GetInstance<IGroupRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _boardForumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _boardCategoryRepository = ObjectFactory.GetInstance<IBoardCategoryRepository>();
            _groupMemberRepository = ObjectFactory.GetInstance<IGroupMemberRepository>();
            _groupService = ObjectFactory.GetInstance<IGroupService>();
        }

        public void Init(IViewGroup view, bool IsPostBack)
        {
            _view = view;
            if(!IsPostBack && _webContext.GroupID > 0)
                LoadData();
        }

        public void LoadData()
        {
            Group group = _groupRepository.GetGroupByID(_webContext.GroupID);
            _view.LoadData(group);

            if(_webContext.CurrentUser != null)
                _view.ShowRequestMembership(true);
            else
                _view.ShowRequestMembership(false);

            //is this public or private data?
            if (group.IsPublic)
            {
                _view.ShowPrivate(true);
                _view.ShowPublic(true);
            }
            else if (ViewerIsMember())
            {
                _view.ShowPrivate(true);
                _view.ShowPublic(true);
            }
            else
            {
                _view.ShowPrivate(false);
                _view.ShowPublic(true);
            }
        }

        public void GoToForum()
        {
            BoardForum forum = _boardForumRepository.GetForumByGroupID(_webContext.GroupID);
            BoardCategory category = _boardCategoryRepository.GetCategoryByCategoryID(forum.CategoryID);
            _redirector.GoToForumsForumView(forum.PageName,category.PageName);
        }

        public void RequestMembership()
        {
            if (_webContext.CurrentUser != null)
            {
                GroupMember gm = new GroupMember();
                gm.AccountID = _webContext.CurrentUser.AccountID;
                gm.GroupID = _webContext.GroupID;
                gm.CreateDate = DateTime.Now;
                gm.IsAdmin = false;
                gm.IsApproved = false;
                _groupMemberRepository.SaveGroupMember(gm);
                _view.ShowMessage("Membership requested successfully!");
            }
        }

        public void ViewMembers()
        {
            _redirector.GoToGroupsMembers(_webContext.GroupID);
        }

        private bool ViewerIsMember()
        {
            bool result = false;
            if (_webContext.CurrentUser != null)
            {
                if (_groupService.IsOwner(_webContext.CurrentUser.AccountID, _webContext.GroupID))
                    result = true;
                else if (_groupService.IsMember(_webContext.CurrentUser.AccountID, _webContext.GroupID))
                    result = true;
            }
            return result;
        }
    }
}
