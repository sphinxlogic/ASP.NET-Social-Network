using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class TagsRepository : ITagsRepository
    {
        private Connection conn;
        public TagsRepository()
        {
            conn = new Connection();
        }

        public Tag GetTagByName(string Name)
        {
            Tag result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Tags.Where(t => t.Name == Name).FirstOrDefault();
            }
            return result;
        }

        public Tag GetTagByID(int TagID)
        {
            Tag result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Tags.Where(t => t.TagID == TagID).FirstOrDefault();
            }
            return result;
        }

        public List<Tag> GetTagsGlobal(int TagsToTake)
        {
            List<Tag> results = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                results = (from t in dc.Tags
                          select t).OrderByDescending(t => t.Count).Distinct().Take(TagsToTake).ToList();
            }
            return results;
        }
        public List<Tag> GetTagsBySystemObject(int SystemObjectID, int TagsToTake)
        {
            List<Tag> results = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                results = (from t in dc.Tags
                          join sot in dc.SystemObjectTags on t.TagID equals sot.TagID
                          where sot.SystemObjectID == SystemObjectID 
                          select t).OrderByDescending(t=>t.Count).Distinct().Take(TagsToTake).ToList();
            }
            return results;
        }

        public List<Tag> GetTagsBySystemObjectAndRecordID(int SystemObjectID, long SystemObjectRecordID)
        {
            List<Tag> results = null;
            using (FisharooDataContext dc = conn.GetContext())
            {
                results = (from t in dc.Tags
                           join sot in dc.SystemObjectTags on t.TagID equals sot.TagID
                           where sot.SystemObjectID == SystemObjectID && sot.SystemObjectRecordID == SystemObjectRecordID
                           select t).OrderBy(t => t.CreateDate).ToList();
            }
            return results;
        }

        public Tag SaveTag(Tag tag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(tag.TagID > 0)
                {
                    dc.Tags.Attach(tag, true);
                }
                else
                {
                    dc.Tags.InsertOnSubmit(tag);
                }
                dc.SubmitChanges();
            }
            return tag;
        }

        public void DeleteTag(Tag tag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.SystemObjectTags.DeleteAllOnSubmit(dc.SystemObjectTags.Where(t => t.TagID == tag.TagID));
                dc.Tags.DeleteOnSubmit(dc.Tags.Where(t=>t.TagID == tag.TagID).FirstOrDefault());
                dc.SubmitChanges();
            }
        }
    }
}
