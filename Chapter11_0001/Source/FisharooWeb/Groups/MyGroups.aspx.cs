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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Groups.Interface;
using Fisharoo.FisharooWeb.Groups.Presenter;

namespace Fisharoo.FisharooWeb.Groups
{
    public partial class MyGroups : System.Web.UI.Page, IMyGroups
    {
        private MyGroupsPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new MyGroupsPresenter();
            _presenter.Init(this);
        }

        public void LoadData(List<Group> groups)
        {
            lvGroups.DataSource = groups;
            lvGroups.DataBind();
        }

        public void ShowMessage(string message)
        {
            lblMessage.Text = message;
        }

        protected void lvGroups_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Image imgGroupImage = e.Item.FindControl("imgGroupImage") as Image;
                Literal litImageID = e.Item.FindControl("litImageID") as Literal;
                Literal litPageName = e.Item.FindControl("litPageName") as Literal;
                LinkButton lbPageName = e.Item.FindControl("lbPageName") as LinkButton;
                ImageButton ibDelete = e.Item.FindControl("ibDelete") as ImageButton;
                Literal litGroupID = e.Item.FindControl("litGroupID") as Literal;
                ImageButton ibEdit = e.Item.FindControl("ibEdit") as ImageButton;

                ibDelete.Attributes.Add("GroupID", litGroupID.Text);
                ibEdit.Attributes.Add("GroupID", litGroupID.Text);
                ibDelete.Attributes.Add("onclick","return confirm('Are you sure you want to delete this group?');");
                lbPageName.Attributes.Add("PageName", litPageName.Text);
                imgGroupImage.ImageUrl = "/files/photos/" + _presenter.GetImageByID(Convert.ToInt64(litImageID.Text), File.Sizes.S);
            }
        }

        protected void lbPageName_Click(object sender, EventArgs e)
        {
            LinkButton lbPageName = sender as LinkButton;
            _presenter.GoToGroup(lbPageName.Attributes["PageName"]);
        }

        protected void ibDelete_Click(object sender, EventArgs e)
        {
            ImageButton ibDelete = sender as ImageButton;
            if(ibDelete != null)
            {
                int GroupID = Convert.ToInt32(ibDelete.Attributes["GroupID"]);
                _presenter.DeleteGroup(GroupID);
            }
        }

        protected void ibEdit_Click(object sender, EventArgs e)
        {
            ImageButton ibEdit = sender as ImageButton;
            if(ibEdit != null)
            {
                int GroupID = Convert.ToInt32(ibEdit.Attributes["GroupID"]);
                _presenter.EditGroup(GroupID);
            }
        }
    }
}
