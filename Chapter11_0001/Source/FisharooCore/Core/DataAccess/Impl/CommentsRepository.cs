using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class CommentsRepository : ICommentsRepository
    {
        private Connection conn;
        public CommentsRepository()
        {
            conn = new Connection();
        }

        public Comment GetCommentByID(long CommentID)
        {
            Comment result = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Comments.Where(c => c.CommentID == CommentID).FirstOrDefault();
            }
            return result;
        }

        public List<Comment> GetCommentsBySystemObject(int SystemObjectID, long SystemObjectRecordID)
        {
            List<Comment> results = null;
            using(FisharooDataContext dc = conn.GetContext())
            {
                results =
                    dc.Comments.Where(
                        c => c.SystemObjectID == SystemObjectID && c.SystemObjectRecordID == SystemObjectRecordID).
                        OrderByDescending(c => c.CreateDate).
                        ToList();
            }
            return results;
        }

        public long SaveComment(Comment comment)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (comment.CommentID > 0)
                {
                    dc.Comments.Attach(comment, true);
                }
                else
                {
                    dc.Comments.InsertOnSubmit(comment);
                }
                dc.SubmitChanges();
            }
            return comment.CommentID;
        }

        public void DeleteComment(Comment comment)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Comments.Attach(comment, true);
                dc.Comments.DeleteOnSubmit(comment);
                dc.SubmitChanges();
            }
        }
    }
}
