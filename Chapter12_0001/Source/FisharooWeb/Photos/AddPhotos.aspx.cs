using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos
{
    public partial class AddPhotos : System.Web.UI.Page
    {
        protected IWebContext _webContext;
        protected IUserSession _userSession;
        protected IRedirector _redirector;
        protected void Page_Load(object sender, EventArgs e)
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();

            if(!(_webContext.AlbumID > 0))
                _redirector.GoToPhotos();
        }

        protected void lbViewAlbum_Click(object sender, EventArgs e)
        {
            _redirector.GoToPhotosViewAlbum(_webContext.AlbumID);
        }

        protected void lbEditPhotos_Click(object sender, EventArgs e)
        {
            _redirector.GoToPhotosEditPhotos(_webContext.AlbumID);
        }
    }
}
