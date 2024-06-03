using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class BoardForumRepository : IBoardForumRepository
    {
        //one update counts method
        //  update thread count
        //  update post count
        private Connection _conn;
        public BoardForumRepository()
        {
            _conn = new Connection();
        }

        public BoardForum GetForumByID(Int32 ForumID)
        {
            BoardForum result;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                BoardForum forum = dc.BoardForums.Where(f => f.ForumID == ForumID).FirstOrDefault();
                result = forum;
            }
            return result;
        }

        public BoardForum GetForumByPageName(string PageName)
        {
            BoardForum result;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                BoardForum forum = dc.BoardForums.Where(f => f.PageName == PageName).FirstOrDefault();
                result = forum;
            }
            return result;
        }

        public List<BoardForum> GetForumsByCategoryID(Int32 CategoryID)
        {
            List<BoardForum> results;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                IEnumerable<BoardForum> forums = dc.BoardForums.Where(f => f.CategoryID == CategoryID);
                results = forums.ToList();
            }
            return results;
        }

        public List<BoardForum> GetAllForums()
        {
            List<BoardForum> results;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                IEnumerable<BoardForum> forums = dc.BoardForums;
                results = forums.ToList();
            }
            return results;
        }

        public Int32 SaveForum(BoardForum boardForum)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                if(boardForum.ForumID > 0)
                {
                    dc.BoardForums.Attach(boardForum, true);
                }
                else
                {
                    dc.BoardForums.InsertOnSubmit(boardForum);
                }
                dc.SubmitChanges();
            }
            return boardForum.ForumID;
        }

        public void DeleteForum(BoardForum boardForum)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                 dc.BoardForums.Attach(boardForum, true);
                 dc.BoardForums.DeleteOnSubmit(boardForum);
                 dc.SubmitChanges();
            }
        }
    }
}
