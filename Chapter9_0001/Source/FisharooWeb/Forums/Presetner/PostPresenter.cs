using System;
using System.Data;
using System.Configuration;
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
using Fisharoo.FisharooWeb.Forums.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Forums.Presetner
{
    public class PostPresenter
    {
        private IPost _view;
        private IBoardPostRepository _postRepository;
        private IBoardForumRepository _forumRepository;
        private IBoardCategoryRepository _categoryRepository;
        private IRedirector _redirector;
        private IWebContext _webContext;
        private IAlertService _alertService;
        public PostPresenter()
        {
            _postRepository = ObjectFactory.GetInstance<IBoardPostRepository>();
            _forumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _categoryRepository = ObjectFactory.GetInstance<IBoardCategoryRepository>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
        }

        public void Init(IPost View)
        {
            _view = View;
            _view.SetDisplay(_webContext.IsThread);
        }

        public void Save(BoardPost post)
        {
            //is new thread
            if(_webContext.ForumID > 0)
            {
                post.ForumID = _webContext.ForumID;
                post.IsThread = _webContext.IsThread;

                if(!_postRepository.CheckPostPageNameIsUnique(post.PageName))
                {
                    _view.SetErrorMessage("The page name you are trying to use is already in use!");
                    return;
                }
            }
            //is reply post
            else
            {
                BoardPost postToReplyToo = _postRepository.GetPostByID(_webContext.PostID);
                if (postToReplyToo.IsThread)
                    post.ThreadID = postToReplyToo.PostID;
                else
                    post.ThreadID = postToReplyToo.ThreadID;

                post.ForumID = postToReplyToo.ForumID;
            }
            post.CreateDate = DateTime.Now;
            post.UpdateDate = DateTime.Now;
            post.AccountID = _webContext.CurrentUser.AccountID;
            post.Username = _webContext.CurrentUser.Username;
            post.ReplyCount = 0;
            post.ViewCount = 0;
            
            post.PostID = _postRepository.SavePost(post);

            BoardForum forum = _forumRepository.GetForumByID(post.ForumID);
            BoardCategory category = _categoryRepository.GetCategoryByCategoryID(forum.CategoryID);
            BoardPost thread;
            if(post.IsThread)
                thread = _postRepository.GetPostByID(post.PostID);
            else
                thread = _postRepository.GetPostByID((long)post.ThreadID);

            //add an alert to the filter
            if(post.IsThread)
                _alertService.AddNewBoardThreadAlert(category,forum,thread);
            else
                _alertService.AddNewBoardPostAlert(category, forum, post, thread);

            _redirector.GoToForumsViewPost(forum.PageName,category.PageName,thread.PageName);
        }
    }
}
