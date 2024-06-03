using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class GroupTypeRepository : IGroupTypeRepository
    {
        private Connection conn;
        public GroupTypeRepository()
        {
            conn = new Connection();
        }

        public List<GroupType> GetAllGroupTypes()
        {
            List<GroupType> result = new List<GroupType>();    
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.GroupTypes.OrderBy(gt => gt.Name).ToList();
            }
            return result;
        }

        public GroupType GetGroupTypeByID(Int32 GroupTypeID)
        {
            GroupType result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.GroupTypes.Where(gt => gt.GroupTypeID == GroupTypeID).FirstOrDefault();
            }
            return result;
        }

        public List<GroupType> GetGroupTypesByGroupID(Int32 GroupID)
        {
            List<GroupType> result = new List<GroupType>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<GroupType> groupTypes = from gt in dc.GroupTypes
                                                    join gtgt in dc.GroupToGroupTypes on gt.GroupTypeID equals
                                                        gtgt.GroupTypeID
                                                    where gtgt.GroupID == GroupID
                                                    select gt;
                result = groupTypes.ToList();
            }
            return result;
        }

        public Int64 SaveGroupType(GroupType groupType)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(groupType.GroupTypeID > 0)
                {
                    dc.GroupTypes.Attach(groupType, true);
                }
                else
                {
                    dc.GroupTypes.InsertOnSubmit(groupType);
                }
                dc.SubmitChanges();
            }
            return groupType.GroupTypeID;
        }

        public void DeleteGroupType(GroupType groupType)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.GroupTypes.DeleteOnSubmit(groupType);
                dc.SubmitChanges();
            }
        }
    }
}
