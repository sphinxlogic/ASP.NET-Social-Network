using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    //CHAPTER 5
    [Pluggable("Default")]
    public class FriendService : IFriendService
    {
        private IFriendInvitationRepository _friendInvitationRepository;
        private IFriendRepository _friendRepository;
        private IAlertService _alertService;
        private IAccountRepository _accountRepository;
        private IWebContext _webContext;
        private IMessageRepository _messageRepository;
        private IMessageRecipientRepository _messageRecipientRepository;

        public FriendService()
        {
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _friendInvitationRepository = ObjectFactory.GetInstance<IFriendInvitationRepository>();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _messageRecipientRepository = ObjectFactory.GetInstance<IMessageRecipientRepository>();
            _messageRepository = ObjectFactory.GetInstance<IMessageRepository>();
        }

        public bool IsFriend(Account account, Account accountBeingViewed)
        {
            if(account == null)
                return false;

            if(accountBeingViewed == null)
                return false;

            if(account.AccountID == accountBeingViewed.AccountID)
                return true;
            else
            {
                Friend friend = _friendRepository.GetFriendsByAccountID(accountBeingViewed.AccountID).Where(f => f.MyFriendsAccountID == account.AccountID).FirstOrDefault();
                if(friend != null)
                    return true;
            }
            return false;
        }

        public void CreateFriendFromFriendInvitation(Guid InvitationKey, Account InvitationTo)
        {
            //update friend invitation request
            FriendInvitation friendInvitation = _friendInvitationRepository.GetFriendInvitationByGUID(InvitationKey);
            friendInvitation.BecameAccountID = InvitationTo.AccountID;
            _friendInvitationRepository.SaveFriendInvitation(friendInvitation);
            _friendInvitationRepository.CleanUpFriendInvitationsForThisEmail(friendInvitation);

            //create friendship
            Friend friend = new Friend();
            friend.AccountID = friendInvitation.AccountID;
            friend.MyFriendsAccountID = InvitationTo.AccountID;
            _friendRepository.SaveFriend(friend);

            Account InvitationFrom = _accountRepository.GetAccountByID(friendInvitation.AccountID);
            _alertService.AddFriendAddedAlert(InvitationFrom, InvitationTo);

            //add a message to the inbox regarding the new friendship!
            Message m = new Message();
            m.Subject = "You and " + InvitationTo.Username + " are now friends!";
            m.Body = "You and <a href=\"" + _webContext.RootUrl + InvitationTo.Username + "\">" + InvitationTo.Username + "</a> are now friends!";
            m.CreateDate = DateTime.Now;
            m.MessageTypeID = (int)MessageTypes.FriendConfirm;
            m.SentByAccountID = InvitationFrom.AccountID;
            Int64 messageID = _messageRepository.SaveMessage(m);

            MessageRecipient mr = new MessageRecipient();
            mr.AccountID = InvitationTo.AccountID;
            mr.MessageFolderID = (int)MessageFolders.Inbox;
            mr.MessageID = messageID;
            mr.MessageRecipientTypeID = (int) MessageRecipientTypes.TO;
            mr.MessageStatusTypeID = (int) MessageStatusTypes.Unread;
            _messageRecipientRepository.SaveMessageRecipient(mr);
        }
    }
}
