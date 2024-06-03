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
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Mail.UserControls.Interface;
using Fisharoo.FisharooWeb.Mail.UserControls.Presenter;

namespace Fisharoo.FisharooWeb.Mail.UserControls
{
    public partial class Folders : System.Web.UI.UserControl, IFolders 
    {
        private FoldersPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new FoldersPresenter();
            _presenter.Init(this);
        }

        public void LoadFolders(List<MessageFolder> Folders)
        {
            repFolders.DataSource = Folders;
            repFolders.DataBind();
        }

        protected void repFolders_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkFolder = e.Item.FindControl("linkFolder") as HyperLink;
                linkFolder.Text = ((MessageFolder)e.Item.DataItem).FolderName;
                linkFolder.NavigateUrl = "~/Mail/Default.aspx?folder=" + ((MessageFolder) e.Item.DataItem).MessageFolderID.ToString();
                linkFolder.Attributes.Add("FolderID", ((MessageFolder)e.Item.DataItem).MessageFolderID.ToString());
            }
        }
    }
}