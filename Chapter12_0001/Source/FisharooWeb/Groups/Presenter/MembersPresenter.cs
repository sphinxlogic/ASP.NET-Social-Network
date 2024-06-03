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
using Fisharoo.FisharooWeb.Groups.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Groups.Presenter
{
    public class MembersPresenter
    {
        private IMembers _view;
        private IAccountRepository _accountRepository;
        private IGroupMemberRepository _groupMemberRepository;
        private IGroupService _groupService;
        private IWebContext _webContext;
        private IRedirector _redirector;
        public MembersPresenter()
        {
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _groupMemberRepository = ObjectFactory.GetInstance<IGroupMemberRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _groupService = ObjectFactory.GetInstance<IGroupService>();
        }

        public void Init(IMembers view, bool IsPostBack)
        {
            _view = view;

            //do we show the buttons?
            if(_webContext.CurrentUser == null)
                _view.SetButtonsVisibility(false);
            else if (_groupService.IsOwnerOrAdministrator(_webContext.CurrentUser.AccountID, _webContext.GroupID))
                _view.SetButtonsVisibility(true);
            else
                _view.SetButtonsVisibility(false);

            if(!IsPostBack)
                LoadData();
        }

        public void Next()
        {
            _redirector.GoToGroupsMembers(_webContext.GroupID,(_webContext.PageNumber + 1));
        }

        public void Previous()
        {
            _redirector.GoToGroupsMembers(_webContext.GroupID,(_webContext.PageNumber - 1));
        }

        public void LoadData()
        {
            _view.LoadData(_accountRepository.GetApprovedAccountsByGroupID(_webContext.GroupID, _webContext.PageNumber),
                           _accountRepository.GetAccountsToApproveByGroupID(_webContext.GroupID));
        }

        public void ApproveMembers(List<int> MemberIDs)
        {
            _groupMemberRepository.ApproveGroupMembers(MemberIDs, _webContext.GroupID);
            LoadData();
            _view.ShowMessage("Members approved!");
        }

        public void DeleteMembers(List<int> MemberIDs)
        {
            _groupMemberRepository.DeleteGroupMembers(MemberIDs, _webContext.GroupID);
            LoadData();
            _view.ShowMessage("Members deleted!");
        }

        public void PromoteMembers(List<int> MemberIDs)
        {
            _groupMemberRepository.PromoteGroupMembersToAdmin(MemberIDs,_webContext.GroupID);
            LoadData();
            _view.ShowMessage("Members promoted!");
        }

        public void DemoteMembers(List<int> MemberIDs)
        {
            _groupMemberRepository.DemoteGroupMembersFromAdmin(MemberIDs,_webContext.GroupID);
            LoadData();
            _view.ShowMessage("Members demoted!");
        }

        public void Back()
        {
            _redirector.GoToGroupsViewGroup(_webContext.GroupID);
        }
    }
}
