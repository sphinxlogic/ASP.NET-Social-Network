﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class BoardPostRepository : IBoardPostRepository
    {
        private Connection _conn;
        public BoardPostRepository()
        {
            _conn = new Connection();
        }

        public BoardPost GetPostByPageName(string PageName)
        {
            BoardPost result = null;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                BoardPost post = dc.BoardPosts.Where(p => p.PageName == PageName).FirstOrDefault();
                result = post;
            }
            return result;
        }

        public BoardPost GetPostByID(Int64 PostID)
        {
            BoardPost result = null;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                BoardPost post = dc.BoardPosts.Where(p => p.PostID == PostID).FirstOrDefault();
                result = post;
            }
            return result;
        }

        public List<BoardPost> GetPostsByThreadID(Int64 ThreadID)
        {
            List<BoardPost> result;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                IEnumerable<BoardPost> posts = dc.BoardPosts.Where(p => p.ThreadID == ThreadID && !p.IsThread).OrderBy(p=>p.CreateDate);
                result = posts.ToList();
            }
            return result;
        }

        public List<BoardPost> GetThreadsByForumID(Int32 ForumID)
        {
            List<BoardPost> result;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                IEnumerable<BoardPost> posts =
                    dc.BoardPosts.Where(p => p.ForumID == ForumID && p.IsThread).OrderBy(p => p.UpdateDate);
                result = posts.ToList();
            }
            result.Reverse();
            return result;
        }

        public void AddPostView(Int64 PostID, Int32 AccountID)
        {
            BoardPostView bpv = new BoardPostView();
            bpv.AccountID = AccountID;
            bpv.CreateDate = DateTime.Now;
            bpv.PostID = PostID;
            
            using(FisharooDataContext dc = _conn.GetContext())
            {
                dc.BoardPostViews.InsertOnSubmit(bpv);
                dc.SubmitChanges();
            }
        }

        public Int64 SavePost(BoardPost boardPost)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                if(boardPost.PostID > 0)
                {
                    dc.BoardPosts.Attach(boardPost, true);
                }
                else
                {
                    //get the parent containers when a new post is created
                    //  to update their post counts
                    BoardCategory bc = (from c in dc.BoardCategories
                                        join f in dc.BoardForums on c.CategoryID equals f.CategoryID
                                        where f.ForumID == boardPost.ForumID
                                        select c).FirstOrDefault();
                    BoardForum bf = (from f in dc.BoardForums
                                     where f.ForumID == boardPost.ForumID
                                     select f).FirstOrDefault();

                    //update the thread count
                    if(boardPost.IsThread)
                    {
                        bc.ThreadCount = bc.ThreadCount + 1;
                        bf.ThreadCount = bf.ThreadCount + 1;
                    }
                    //update the post count
                    else
                    {
                        bc.PostCount = bc.PostCount + 1;
                        bf.PostCount = bf.PostCount + 1;

                        //update post count on thread
                        BoardPost bThread = null;
                        if (boardPost.ThreadID != 0)
                        {
                            bThread = (from p in dc.BoardPosts
                                       where p.PostID == boardPost.ThreadID
                                       select p).FirstOrDefault();
                        }
                        if (bThread != null)
                        {
                            bThread.ReplyCount = bThread.ReplyCount + 1;
                        }
                    }

                    dc.BoardPosts.InsertOnSubmit(boardPost);
                }
                dc.SubmitChanges();
            }
            return boardPost.PostID;
        }

        public void DeletePost(BoardPost boardPost)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                dc.BoardPosts.Attach(boardPost, true);
                dc.BoardPosts.DeleteOnSubmit(boardPost);
                dc.SubmitChanges();
            }
        }
    }
}
