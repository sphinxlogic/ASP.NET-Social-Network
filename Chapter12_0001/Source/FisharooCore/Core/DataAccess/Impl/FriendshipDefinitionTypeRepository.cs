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
    public class FriendshipDefinitionTypeRepository : IFriendshipDefinitionTypeRepository
    {
        private Connection conn;
        public FriendshipDefinitionTypeRepository()
        {
            conn = new Connection();
        }

        public FriendshipDefinitionType GetFriendshipDefinitionTypeByID(Int32 FriendshipDefinitionTypeID)
        {
            FriendshipDefinitionType result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.FriendshipDefinitionTypes.Where(fdt => fdt.FriendshipDefinitionTypeID == FriendshipDefinitionTypeID).FirstOrDefault();
            }
            return result;
        }
    }
}
