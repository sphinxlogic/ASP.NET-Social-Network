using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooWeb.Handlers
{
    //CHAPTER 8
    public class UrlRewrite : IHttpModule
    {
        private IAccountRepository _accountRepository;
        private IBlogRepository _blogRepository;
        private IBoardCategoryRepository _categoryRepository;
        private IBoardForumRepository _forumRepository;
        private IBoardPostRepository _postRepository;
        private IWebContext _webContext;
        public UrlRewrite()
        {
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _blogRepository = ObjectFactory.GetInstance<IBlogRepository>();
            _categoryRepository = ObjectFactory.GetInstance<IBoardCategoryRepository>();
            _forumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _postRepository = ObjectFactory.GetInstance<IBoardPostRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(HttpApplication application)
        {
            //let's register our event handler
            application.PostResolveRequestCache +=
                (new EventHandler(this.Application_OnAfterProcess));
        }

        public void Dispose()
        {
            
        }

        private void Application_OnAfterProcess(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            string[] extensionsToExclude = { ".axd", ".jpg", ".gif", ".png", ".xml", ".config", ".css", ".js", ".htm", ".html" };
            foreach (string s in extensionsToExclude)
            {
                if (application.Request.PhysicalPath.ToLower().Contains(s))
                    return;
            }

            if (!System.IO.File.Exists(application.Request.PhysicalPath))
            {
                if (application.Request.PhysicalPath.ToLower().Contains("blogs"))
                {
                    string[] arr = application.Request.PhysicalPath.ToLower().Split('\\');
                    string blogPageName = arr[arr.Length - 1];
                    string blogUserName = arr[arr.Length - 2];
                    blogPageName = blogPageName.Replace(".aspx", "");

                    if (blogPageName.ToLower() != "profileimage" && blogUserName.ToLower() != "profileavatar")
                    {
                        Account account = _accountRepository.GetAccountByUsername(blogUserName);
                        Blog blog = _blogRepository.GetBlogByPageName(blogPageName, account.AccountID);

                        context.RewritePath("~/blogs/ViewPost.aspx?BlogID=" + blog.BlogID.ToString());
                    }
                    else
                    {
                        return;
                    }
                }
                else if (application.Request.PhysicalPath.ToLower().Contains("forums"))
                {
                    string[] arr = application.Request.PhysicalPath.ToLower().Split('\\');
                    int forumsPosition = 0;
                    int itemsAfterForums = 0;
                    string categoryPageName = "";
                    string forumPageName = "";
                    string postPageName = "";

                    for (int i = 0; i < arr.Length;i++ )
                    {
                        if(arr[i].ToLower() == "forums")
                        {
                            forumsPosition = i;
                            break;
                        }
                    }

                    itemsAfterForums = (arr.Length - 1) - forumsPosition;

                    if (itemsAfterForums == 2)
                    {
                        categoryPageName = arr[arr.Length - 2];
                        forumPageName = arr[arr.Length - 1];
                        forumPageName = forumPageName.Replace(".aspx", "");
                        BoardForum forum = _forumRepository.GetForumByPageName(forumPageName);
                        context.RewritePath("/forums/ViewForum.aspx?ForumID=" + forum.ForumID.ToString() +
                                            "&CategoryPageName=" + categoryPageName + "&ForumPageName=" + forumPageName, true);
                    }
                    else if (itemsAfterForums == 3)
                    {
                        categoryPageName = arr[arr.Length - 3];
                        forumPageName = arr[arr.Length - 2];
                        postPageName = arr[arr.Length - 1];
                        postPageName = postPageName.Replace(".aspx", "");
                        BoardPost post = _postRepository.GetPostByPageName(postPageName);
                        context.RewritePath("/forums/ViewPost.aspx?PostID=" + post.PostID.ToString(), true);
                    }
                }
                else
                {
                    string username = application.Request.Path.Replace("/", "");
                    Account account = _accountRepository.GetAccountByUsername(username);
                    if (account != null)
                    {
                        string UserURL = "~/Profiles/profile.aspx?AccountID=" + account.AccountID.ToString();
                        context.Response.Redirect(UserURL);
                    }
                    else
                    {
                        context.Response.Redirect("~/PageNotFound.aspx");
                    }
                }
            }
        }
    }
}
