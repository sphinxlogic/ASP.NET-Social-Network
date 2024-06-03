using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IBoardPostRepository
    {
        BoardPost GetPostByPageName(string PageName);
        BoardPost GetPostByID(Int64 PostID);
        Int64 SavePost(BoardPost boardPost);
        void DeletePost(BoardPost boardPost);
        List<BoardPost> GetPostsByThreadID(Int64 ThreadID);
        List<BoardPost> GetThreadsByForumID(Int32 ForumID);
        bool CheckPostPageNameIsUnique(string PageName);
    }
}