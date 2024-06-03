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
using Fisharoo.FisharooWeb.Forums.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Forums.Presetner
{
    public class ViewPostPresenter
    {
        private IViewPost _view;
        private IWebContext _webContext;
        private IBoardPostRepository _postRepository;
        public ViewPostPresenter()
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _postRepository = ObjectFactory.GetInstance<IBoardPostRepository>();
        }

        public void Init(IViewPost View)
        {
            _view = View;
            LoadData();
        }

        private void LoadData()
        {
            _view.LoadData(_postRepository.GetPostByID(_webContext.PostID),_postRepository.GetPostsByThreadID(_webContext.PostID));
        }
    }
}
