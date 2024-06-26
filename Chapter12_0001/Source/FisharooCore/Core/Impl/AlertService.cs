﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using stdole;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class AlertService : IAlertService
    {
        private IUserSession _userSession;
        private IAlertRepository _alertRepository;
        private IWebContext _webContext;
        private IConfiguration _configuration;
        private IFriendRepository _friendRepository;
        private IGroupMemberRepository _groupMemberRepository;

        private Account account;
        private Alert alert;
        private string alertMessage;
        private string[] tags = {"[rootUrl]"};
        public AlertService()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _alertRepository = ObjectFactory.GetInstance<IAlertRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _groupMemberRepository = ObjectFactory.GetInstance<IGroupMemberRepository>();
        }

        private void Init()
        {
            account = _userSession.CurrentUser;

            alert = new Alert();
            if(_userSession.CurrentUser != null)
                alert.AccountID = account.AccountID;
            alert.CreateDate = DateTime.Now;
        }

        private void Init(Account modifiedAccount)
        {
            account = modifiedAccount;

            alert = new Alert();
            alert.AccountID = account.AccountID;
            alert.CreateDate = DateTime.Now;
        }

        //CHAPTER 10
        public void AddNewBoardPostAlert(BoardCategory category, BoardForum forum, BoardPost post, BoardPost thread, Group group)
        {
            Init();
            alert.AlertTypeID = (int)AlertType.AlertTypes.NewBoardPost;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has just added a new post: <b>" +
                           post.Name + "</b></div>";

            alertMessage += "<div class=\"AlertRow\"><a href=\"" + _webContext.RootUrl + "forums/" + category.PageName +
                           "/" + forum.PageName + "/" + thread.PageName + ".aspx" + "\">" + _webContext.RootUrl +
                           "forums/" + category.PageName + "/" + forum.PageName + "/" + thread.PageName +
                           ".aspx</a></div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToGroup(alert,group);
        }

        //CHAPTER 10
        public void AddNewBoardThreadAlert(BoardCategory category, BoardForum forum, BoardPost post, Group group)
        {
            Init();
            alert.AlertTypeID = (int)AlertType.AlertTypes.NewBoardThread;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has just added a new thread on the board: <b>" +
                           post.Name + "</b></div>";

            alertMessage += "<div class=\"AlertRow\"><a href=\"" + _webContext.RootUrl + "forums/" + category.PageName +
                           "/" + forum.PageName + "/" + post.PageName + ".aspx" + "\">" + _webContext.RootUrl +
                           "forums/" + category.PageName + "/" + forum.PageName + "/" + post.PageName +
                           ".aspx</a></div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToGroup(alert, group);
        }

        //CHAPTER 9
        public void AddNewBoardPostAlert(BoardCategory category, BoardForum forum, BoardPost post, BoardPost thread)
        {
            Init();
            alert.AlertTypeID = (int) AlertType.AlertTypes.NewBoardPost;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has just added a new post: <b>" +
                           post.Name + "</b></div>";

            alertMessage += "<div class=\"AlertRow\"><a href=\"" + _webContext.RootUrl + "forums/" + category.PageName +
                           "/" + forum.PageName + "/" + thread.PageName + ".aspx" + "\">" + _webContext.RootUrl +
                           "forums/" + category.PageName + "/" + forum.PageName + "/" + thread.PageName +
                           ".aspx</a></div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        //CHAPTER 9
        public void AddNewBoardThreadAlert(BoardCategory category, BoardForum forum, BoardPost post)
        {
            Init();
            alert.AlertTypeID = (int)AlertType.AlertTypes.NewBoardThread;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has just added a new thread on the board: <b>" +
                           post.Name + "</b></div>";

            alertMessage += "<div class=\"AlertRow\"><a href=\"" + _webContext.RootUrl + "forums/" + category.PageName +
                           "/" + forum.PageName + "/" + post.PageName + ".aspx" + "\">" + _webContext.RootUrl +
                           "forums/" + category.PageName + "/" + forum.PageName + "/" + post.PageName +
                           ".aspx</a></div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        //CHAPTER 8
        public void AddNewBlogPostAlert(Blog blog)
        {
            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = _userSession.CurrentUser.AccountID;
            alert.AlertTypeID = (int)AlertType.AlertTypes.NewBlogPost;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has just added a new blog post: <b>" +
                           blog.Title + "</b></div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        //CHAPTER 8
        public void AddUpdatedBlogPostAlert(Blog blog)
        {
            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = _userSession.CurrentUser.AccountID;
            alert.AlertTypeID = (int)AlertType.AlertTypes.NewBlogPost;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " has updated the <b>" + blog.Title +
                           "</b> blog post!</div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        //CHAPTER 5
        public void AddStatusUpdateAlert(StatusUpdate statusUpdate)
        {
            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = _userSession.CurrentUser.AccountID;
            alert.AlertTypeID = (int)AlertType.AlertTypes.StatusUpdate;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(_userSession.CurrentUser.AccountID) +
                           GetProfileUrl(_userSession.CurrentUser.Username) + " " + statusUpdate.Status + "</div>";
            alert.Message = alertMessage;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        //CHAPTER 5
        public void AddFriendRequestAlert(Account FriendRequestFrom, Account FriendRequestTo, Guid requestGuid, string Message)
        {
            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = FriendRequestTo.AccountID;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(FriendRequestFrom.AccountID) +
                           GetProfileUrl(FriendRequestFrom.Username) + " would like to be friends!</div>";
            alertMessage += "<div class=\"AlertRow\">";
            alertMessage += FriendRequestFrom.FirstName + " " + FriendRequestFrom.LastName +
                            " would like to be friends with you!  Click this link to add this user as a friend: ";
            alertMessage += "<a href=\"" + _configuration.RootURL +
            "Friends/ConfirmFriendshipRequest.aspx?InvitationKey=" + requestGuid.ToString() + "\">" + _configuration.RootURL +
            "Friends/ConfirmFriendshipRequest.aspx?InvitationKey=" + requestGuid.ToString() + "</a><HR>" + Message + "</div>";

            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.FriendRequest;
            SaveAlert(alert);
        }

        //CHAPTER 5
        public void AddFriendAddedAlert(Account FriendRequestFrom, Account FriendRequestTo)
        {
            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = FriendRequestFrom.AccountID;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(FriendRequestTo.AccountID) +
                           GetProfileUrl(FriendRequestTo.Username) + " is now your friend!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(FriendRequestTo.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int)AlertType.AlertTypes.FriendAdded;
            SaveAlert(alert);

            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AccountID = FriendRequestTo.AccountID;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileImage(FriendRequestFrom.AccountID) +
                           GetProfileUrl(FriendRequestFrom.Username) + " is now your friend!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(FriendRequestFrom.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int)AlertType.AlertTypes.FriendAdded;
            SaveAlert(alert);

            alert = new Alert();
            alert.CreateDate = DateTime.Now;
            alert.AlertTypeID = (int) AlertType.AlertTypes.FriendAdded;
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(FriendRequestFrom.Username) + " and " +
                           GetProfileUrl(FriendRequestTo.Username) + " are now friends!</div>";
            alert.Message = alertMessage;

            alert.AccountID = FriendRequestFrom.AccountID;
            SendAlertToFriends(alert);

            alert.AccountID = FriendRequestTo.AccountID;
            SendAlertToFriends(alert);
        }

        public void AddAccountCreatedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) + " just signed up!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(account.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int)AlertType.AlertTypes.AccountCreated;
            SaveAlert(alert);
        }

        public void AddAccountModifiedAlert(Account modifiedAccount)
        {
            Init(modifiedAccount);
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " modified their account.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.AccountModified;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        public void AddProfileCreatedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " just created their profile!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(account.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.ProfileCreated;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        public void AddProfileModifiedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " modified their profile.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.ProfileModified;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        public void AddNewAvatarAlert()
        {
            Init();
            alertMessage =
                "<div class=\"AlertHeader\">" + GetProfileImage(account.AccountID) + GetProfileUrl(account.Username) +
                " added a new avatar.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.NewAvatar;
            SaveAlert(alert);
            SendAlertToFriends(alert);
        }

        public List<Alert> GetAlertsByAccountID(Int32 AccountID)
        {
            List<Alert> result = new List<Alert>();
            List<Alert> alerts = _alertRepository.GetAlertsByAccountID(AccountID);
            foreach (Alert alert in alerts)
            {
                foreach (string s in tags)
                {
                    switch(s)
                    {
                        case "[rootUrl]":
                            alert.Message = alert.Message.Replace("[rootUrl]", _webContext.RootUrl);
                            result.Add(alert);
                            break;
                    }
                }
            }

            return result;
        }

        private void SaveAlert(Alert alert)
        {
            _alertRepository.SaveAlert(alert);
        }

        private void SendAlertToGroup(Alert alert, Group group)
        {
            List<int> groupMembers = _groupMemberRepository.GetMemberAccountIDsByGroupID(group.GroupID);
            foreach (int id in groupMembers)
            {
                alert.AlertID = 0;
                alert.AccountID = id;
                SaveAlert(alert);
            }
        }

        private void SendAlertToFriends(Alert alert)
        {
            List<Friend> friends = _friendRepository.GetFriendsByAccountID(alert.AccountID);
            foreach (Friend friend in friends)
            {
                alert.AlertID = 0;
                alert.AccountID = friend.MyFriendsAccountID;
                SaveAlert(alert);
            }
        }

        private string GetProfileImage(Int32 AccountID)
        {
            return "<img width=\"50\" height=\"50\" src=\"[rootUrl]images/ProfileAvatar/ProfileImage.aspx?AccountID=" +
                   AccountID.ToString() + "&w=50&h=50\" align=\"absmiddle\">";   
        }

        private string GetProfileUrl(string Username)
        {
            return "<a href=\"[rootUrl]" + Username + "\">" + Username + "</a>";
        }

        //TODO: MESSAGING - point to send message URL
        private string GetSendMessageUrl(Int32 AccountID)
        {
            return "<a href=\"[rootUrl]/mail/newmessage.aspx?AccountID=" + AccountID.ToString() +
                   "\">Click here to send message</a>";
        }
    }
}
