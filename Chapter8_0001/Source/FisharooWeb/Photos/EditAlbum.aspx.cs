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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Photos.Interface;
using Fisharoo.FisharooWeb.Photos.Presenter;

namespace Fisharoo.FisharooWeb.Photos
{
    public partial class EditAlbum : System.Web.UI.Page, IEditAlbum 
    {
        private EditAlbumPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new EditAlbumPresenter();
            _presenter.Init(this, IsPostBack);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.SaveAlbum(txtFolderName.Text, txtDescription.Text, txtLocation.Text);
        }

        public void LoadUI(Folder folder)
        {
            txtFolderName.Text = folder.Name;
            txtDescription.Text = folder.Description;
            txtLocation.Text = folder.Location;
            litFolderID.Text = folder.FolderID.ToString();
        }
    }
}
