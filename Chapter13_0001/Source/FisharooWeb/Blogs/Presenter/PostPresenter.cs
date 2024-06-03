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
using Fisharoo.FisharooWeb.Blogs.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Blogs.Presenter
{
    public class PostPresenter
    {
        private IBlogRepository _blogRepository;
        private IWebContext _webContext;
        private IPost _view;
        public PostPresenter()
        {
            _blogRepository = ObjectFactory.GetInstance<IBlogRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IPost View)
        {
            _view = View;
            if(_webContext.BlogID > 0)
            {
                _view.LoadPost(_blogRepository.GetBlogByBlogID(_webContext.BlogID));
            }
        }

        public void SavePost(Blog blog)
        {
            bool result = _blogRepository.CheckPageNameIsUnique(blog);
            if (result)
            {
                blog.AccountID = _webContext.CurrentUser.AccountID;
                _blogRepository.SaveBlog(blog);
            }
            else
            {
                _view.ShowError("The page name you have chosen is in use.  Please choose a different page name!");
            }
        }
    }
}
