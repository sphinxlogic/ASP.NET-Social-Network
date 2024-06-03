using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class GroupForumRepository : IGroupForumRepository
    {
        private Connection conn;
        public GroupForumRepository()
        {
            conn = new Connection();
        }

        public int GetGroupIdByForumID(int ForumID)
        {
            int result = 0;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.GroupForums.Where(gf => gf.ForumID == ForumID).Select(gf => gf.GroupID).FirstOrDefault();
            }
            return result;
        }

        public void SaveGroupForum(GroupForum groupForum)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (dc.GroupForums.Where(gf=>gf.ForumID == groupForum.ForumID && gf.GroupID == groupForum.GroupID).FirstOrDefault() == null)
                {
                    dc.GroupForums.InsertOnSubmit(groupForum);
                    dc.SubmitChanges();
                }
            }
        }

        public void DeleteGroupForum(int ForumID, int GroupID)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.GroupForums.DeleteAllOnSubmit(dc.GroupForums.Where(gf=>gf.GroupID == GroupID && gf.ForumID == ForumID));
                dc.SubmitChanges();
            }
        }

        public void DeleteGroupForum(GroupForum groupForum)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.GroupForums.Attach(groupForum, true);
                dc.GroupForums.DeleteOnSubmit(groupForum);
                dc.SubmitChanges();
            }
        }
    }
}
