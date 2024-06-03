using System;
using System.Collections;
using System.Collections.Generic;
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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Photos.Interface;
using Fisharoo.FisharooWeb.Photos.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Photos
{
    public partial class Default : System.Web.UI.Page, IDefault 
    {
        private DefaultPresenter _presenter;
        protected IWebContext _webContext;
        protected void Page_Load(object sender, EventArgs e)
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _presenter = new DefaultPresenter();
            _presenter.Init(this);
        }

        public void LoadUI(List<Folder> folders)
        {
            if (!IsPostBack)
            {
                lvAlbums.DataSource = folders;
                lvAlbums.DataBind();
            }
        }

        protected void lbAlbums_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Literal litFolderID = e.Item.FindControl("litFolderID") as Literal;
                Literal litFullPath = e.Item.FindControl("litFullPath") as Literal;
                HyperLink linkAuthor = e.Item.FindControl("linkAuthor") as HyperLink;
                HyperLink linkGallery = e.Item.FindControl("linkGallery") as HyperLink;
                Label lblDescription = e.Item.FindControl("lblDescription") as Label;

                if (lblDescription.Text.Length > 150)
                {
                    lblDescription.Text = lblDescription.Text.Substring(0, 149);
                    lblDescription.Text += "...";
                }
                
                linkAuthor.NavigateUrl = "~/" + linkAuthor.Text;
                linkAuthor.Text = "by - " + linkAuthor.Text;
                linkGallery.NavigateUrl = "~/Photos/ViewAlbum.aspx?AlbumID=" + litFolderID.Text;
                linkGallery.ImageUrl = _webContext.RootUrl + "files/photos/" + litFullPath.Text;
            }
        }
    }
}
