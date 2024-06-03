using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class GroupRepository : IGroupRepository
    {
        private Connection conn;
        public GroupRepository()
        {
            conn = new Connection();
        }
        
        public Group GetGroupByForumID(int ForumID)
        {
            Group result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = (from g in dc.Groups
                          join f in dc.GroupForums on g.GroupID equals f.GroupID
                          where f.ForumID == ForumID
                          select g).FirstOrDefault();
            }
            return result;
        }

        public bool IsOwner(int AccountID, int GroupID)
        {
            bool result = false;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (dc.Groups.Where(g => g.AccountID == AccountID && g.GroupID == GroupID).FirstOrDefault() != null)
                    result = true;
            }
            return result;
        }

        public List<Group> GetLatestGroups()
        {
            List<Group> results = new List<Group>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Group> groups = dc.Groups.OrderByDescending(g => g.UpdateDate).Take(50);
                results = groups.ToList();
            }
            return results;
        }

        public bool CheckIfGroupPageNameExists(string PageName)
        {
            bool result = false;        
            using (FisharooDataContext dc = conn.GetContext())
            {
                Group group = dc.Groups.Where(g => g.PageName == PageName).FirstOrDefault();
                if(group == null)
                    result = false;
            }
            return result;
        }

        public List<Group> GetGroupsAccountIsMemberOf(Int32 AccountID)
        {
            List<Group> result = new List<Group>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Group> groups = from g in dc.Groups
                                            join m in dc.GroupMembers on g.GroupID equals m.GroupID
                                            where m.AccountID == AccountID
                                            select g;
                result = groups.ToList();
            }
            return result;
        }

        public List<Group> GetGroupsOwnedByAccount(Int32 AccountID)
        {
            List<Group> result = new List<Group>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Group> groups = dc.Groups.Where(g => g.AccountID == AccountID);
                result = groups.ToList();
            }
            return result;
        }
        public Group GetGroupByID(Int32 GroupID)
        {
            Group result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Groups.Where(g => g.GroupID == GroupID).FirstOrDefault();
            }
            return result;
        }

        public Group GetGroupByPageName(string PageName)
        {
            Group result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Groups.Where(g => g.PageName == PageName).FirstOrDefault();
            }
            return result;
        }
        
        public Int32 SaveGroup(Group group)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                group.UpdateDate = DateTime.Now;
                if(group.GroupID > 0)
                {
                    dc.Groups.Attach(group, true);    
                }
                else
                {
                    group.CreateDate = DateTime.Now;
                    dc.Groups.InsertOnSubmit(group);
                }
                dc.SubmitChanges();
            }
            return group.GroupID;
        }

        public void DeleteGroup(Group group)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Groups.Attach(group, true);
                dc.Groups.DeleteOnSubmit(group);
                dc.SubmitChanges();
            }
        }

        public void DeleteGroup(int GroupID)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                Group group = dc.Groups.Where(g => g.GroupID == GroupID).FirstOrDefault();
                dc.Groups.DeleteOnSubmit(group);
                dc.SubmitChanges();
            }
        }
    }
}
