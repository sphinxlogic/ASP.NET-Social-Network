using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.MobileControls;
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
    public class ViewForumPresenter
    {
        private IBoardPostRepository _postRepository;
        private IViewForum _view;
        private IWebContext _webContext;
        public ViewForumPresenter()
        {
            _postRepository = ObjectFactory.GetInstance<IBoardPostRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IViewForum view)
        {
            _view = view;
            LoadThreads();
        }

        private void LoadThreads()
        {
            _view.LoadDisplay(_postRepository.GetThreadsByForumID(_webContext.ForumID),_webContext.CategoryPageName,_webContext.ForumPageName, _webContext.ForumID);
        }
    }
}
