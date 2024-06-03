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
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooWeb.Blogs.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Blogs.Presenter
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IBlogRepository _blogRepository;
        public DefaultPresenter()
        {
            _blogRepository = ObjectFactory.GetInstance<IBlogRepository>();
        }

        public void Init(IDefault View)
        {
            _view = View;
            _view.LoadBlogs(_blogRepository.GetLatestBlogs());
        }
    }
}
