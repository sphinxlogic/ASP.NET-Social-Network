using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IBlogRepository
    {
        List<Blog> GetBlogsByAccountID(Int32 AccountID);
        Blog GetBlogByBlogID(Int64 BlogID);
        Int64 SaveBlog(Blog blog);
        void DeleteBlog(Blog blog);
        List<Blog> GetLatestBlogs();
        Blog GetBlogByPageName(string PageName, Int32 AccountID);
        bool CheckPageNameIsUnique(Blog blog);
        void DeleteBlog(Int64 BlogID);
    }
}