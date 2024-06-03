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
using Fisharoo.FisharooWeb.Forum.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Forum.Presetner
{
    public class DefaultPresenter
    {
        private IBoardService _boardService;
        private IDefault _view;
        private IRedirector _redirector;
        private IBoardForumRepository _forumRepository;
        private IBoardCategoryRepository _categoryRepository;
        public DefaultPresenter()
        {
            _boardService = ObjectFactory.GetInstance<IBoardService>();
            _forumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
            _categoryRepository = ObjectFactory.GetInstance<IBoardCategoryRepository>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }

        public void Init(IDefault View)
        {
            _view = View;
            _view.LoadCategories(_boardService.GetCategoriesWithForums());
        }

        public void GoToForum(string ForumPageName)
        {
            BoardForum forum = _forumRepository.GetForumByPageName(ForumPageName);
            BoardCategory category = _categoryRepository.GetCategoryByCategoryID(forum.CategoryID);
            _redirector.GoToForumsForumView(forum.PageName,category.PageName);
        }
    }
}
