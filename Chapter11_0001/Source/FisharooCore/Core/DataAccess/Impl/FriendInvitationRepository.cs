using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    //CHAPTER 5
    [Pluggable("Default")]
    public class FriendInvitationRepository : IFriendInvitationRepository
    {
        private Connection conn;
        public FriendInvitationRepository()
        {
            conn = new Connection();
        }

        public List<FriendInvitation> GetFriendInvitationsByAccountID(Int32 AccountID)
        {
            List<FriendInvitation> result = new List<FriendInvitation>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<FriendInvitation> friendInvitations = dc.FriendInvitations.Where(fi => fi.AccountID == AccountID);
                result = friendInvitations.ToList();
            }
            return result;
        }

        public FriendInvitation GetFriendInvitationByGUID(Guid guid)
        {
            FriendInvitation friendInvitation;
            using(FisharooDataContext dc = conn.GetContext())
            {
                friendInvitation = dc.FriendInvitations.Where(fi => fi.GUID == guid).FirstOrDefault();
            }
            return friendInvitation;
        }

        public void SaveFriendInvitation(FriendInvitation friendInvitation)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (friendInvitation.InvitationID > 0)
                {
                    dc.FriendInvitations.Attach(friendInvitation, true);
                }
                else
                {
                    friendInvitation.CreateDate = DateTime.Now;
                    dc.FriendInvitations.InsertOnSubmit(friendInvitation);
                }
                dc.SubmitChanges();
            }
        }

        //removes multiple requests by the same account to the same email account
        public void CleanUpFriendInvitationsForThisEmail(FriendInvitation friendInvitation)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<FriendInvitation> friendInvitations = from fi in dc.FriendInvitations 
                                                                  where fi.Email == friendInvitation.Email && 
                                                                    fi.BecameAccountID == 0 && 
                                                                    fi.AccountID == friendInvitation.AccountID
                                                                    select fi;
                foreach (FriendInvitation invitation in friendInvitations)
                {
                    dc.FriendInvitations.DeleteOnSubmit(invitation);
                }
                dc.SubmitChanges();
            }
        }
    }
}
