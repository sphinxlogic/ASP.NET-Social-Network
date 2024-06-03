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
    public class FriendshipDefinitionRepository : IFriendshipDefinitionRepository
    {
        private Connection conn;
        public FriendshipDefinitionRepository()
        {
            conn = new Connection();
        }

        public FriendshipDefinition GetFriendshipDefinitionByID(Int32 FriendshipDefinitionID)
        {
            FriendshipDefinition result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.FriendshipDefinitions.Where(fd => fd.FriendshipDefinitionID == FriendshipDefinitionID).FirstOrDefault();
            }
            return result;
        }

        public List<FriendshipDefinition> GetFriendshipDefinitionsByFriendID(Int32 FriendID)
        {
            List<FriendshipDefinition> result = new List<FriendshipDefinition>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<FriendshipDefinition> friendshipDefinitions = dc.FriendshipDefinitions.Where(fd => fd.FriendID == FriendID);
                result = friendshipDefinitions.ToList();
            }
            return result;
        }

        public void SaveFriendshipDefinition(FriendshipDefinition friendshipDefinition)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(friendshipDefinition.FriendshipDefinitionID > 0)
                {
                    dc.FriendshipDefinitions.Attach(friendshipDefinition, true);
                }
                else
                {
                    friendshipDefinition.CreateDate = DateTime.Now;
                    dc.FriendshipDefinitions.InsertOnSubmit(friendshipDefinition);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteFriendshipDefinintion(FriendshipDefinition friendshipDefinition)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.FriendshipDefinitions.DeleteOnSubmit(friendshipDefinition);
                dc.SubmitChanges();
            }
        }
    }
}
