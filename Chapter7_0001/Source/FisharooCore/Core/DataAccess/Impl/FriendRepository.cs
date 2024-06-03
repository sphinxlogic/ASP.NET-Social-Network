using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    //CHAPTER 5
    [Pluggable("Default")]
    public class FriendRepository : IFriendRepository
    {
        private Connection conn;
        public FriendRepository()
        {
            conn = new Connection();
        }

        public Friend GetFriendByID(Int32 FriendID)
        {
            Friend result;

            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Friends.Where(f => f.FriendID == FriendID).FirstOrDefault();
            }

            return result;
        }

        public List<Friend> GetFriendsByAccountID(Int32 AccountID)
        {
            List<Friend> result = new List<Friend>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                //Get my friends direct relationship
                IEnumerable<Friend> friends = (from f in dc.Friends
                                               where f.AccountID == AccountID &&
                                               f.MyFriendsAccountID != AccountID
                                               select f).Distinct();
                result = friends.ToList();

                //Getmy friends indirect relationship
                var friends2 = (from f in dc.Friends
                                where f.MyFriendsAccountID == AccountID &&
                                f.AccountID != AccountID
                                select new
                                {
                                    FriendID = f.FriendID,
                                    AccountID = f.MyFriendsAccountID,
                                    MyFriendsAccountID = f.AccountID,
                                    CreateDate = f.CreateDate,
                                    Timestamp = f.Timestamp
                                }).Distinct();

                foreach (var o in friends2)
                {
                    Friend friend = new Friend(){FriendID = o.FriendID, AccountID = o.AccountID, 
                    CreateDate = o.CreateDate, MyFriendsAccountID = o.MyFriendsAccountID, 
                    Timestamp = o.Timestamp};
                        result.Add(friend);
                }
            }
            return result;
        }

        public List<Account> GetFriendsAccountsByAccountID(Int32 AccountID)
        {
            List<Friend> friends = GetFriendsByAccountID(AccountID);
            List<int> accountIDs = new List<int>();
            foreach (Friend friend in friends)
            {
                accountIDs.Add(friend.MyFriendsAccountID);
            }

            List<Account> result = new List<Account>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Account> accounts = from a in dc.Accounts 
                                                where accountIDs.Contains(a.AccountID)
                                                select a;
                result = accounts.ToList();
            }
            return result;
        }

        public void SaveFriend(Friend friend)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(friend.FriendID > 0)
                {
                    dc.Friends.Attach(friend, true);
                }
                else
                {
                    friend.CreateDate = DateTime.Now;
                    dc.Friends.InsertOnSubmit(friend);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteFriend(Friend friend)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Friends.Attach(friend,true);
                dc.Friends.DeleteOnSubmit(friend);
                dc.SubmitChanges();
            }
        }

        public void DeleteFriendByID(Int32 AccountIDToRemoveFriendFrom, Int32 FriendIDToRemove)
        {
            List<Friend> workingList = new List<Friend>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                //get all friend relationships
                IEnumerable<Friend> friends = from f in dc.Friends
                                    where (f.AccountID == AccountIDToRemoveFriendFrom &&
                                    f.MyFriendsAccountID == FriendIDToRemove) ||
                                    (f.AccountID == FriendIDToRemove &&
                                    f.MyFriendsAccountID == AccountIDToRemoveFriendFrom)
                                    select f;

                workingList = friends.ToList();
            }

            foreach (Friend friend in workingList)
            {
                DeleteFriend(friend);
            }
        }
    }
}
