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

        public FriendService()
        {
            _friendRepository = ObjectFactory.GetInstance<IFriendRepository>();
            _friendInvitationRepository = ObjectFactory.GetInstance<IFriendInvitationRepository>();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
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

            //CHAPTER 6
            //TODO: MESSAGING - Add message to inbox regarding new friendship!
        }
    }
}
