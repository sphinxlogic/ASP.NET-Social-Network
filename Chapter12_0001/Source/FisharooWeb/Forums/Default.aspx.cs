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
using Fisharoo.FisharooWeb.Forum.Interface;
using Fisharoo.FisharooWeb.Forum.Presetner;

namespace Fisharoo.FisharooWeb.Forum
{
    public partial class Default : System.Web.UI.Page, IDefault
    {
        private DefaultPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultPresenter();
            _presenter.Init(this);
        }

        public void LoadCategories(List<BoardCategory> Categories)
        {
            repCategories.DataSource = Categories;
            repCategories.DataBind();
        }

        public void repCategories_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((BoardCategory) e.Item.DataItem).Forums != null)
                {
                    Repeater repForums = e.Item.FindControl("repForums") as Repeater;
                    repForums.DataSource = ((BoardCategory) e.Item.DataItem).Forums;
                    repForums.DataBind();
                }
            }
        }

        public void repForums_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litPageName = e.Item.FindControl("litPageName") as Literal;
                LinkButton lbForum = e.Item.FindControl("lbForum") as LinkButton;
                lbForum.Attributes.Add("ForumPageName",litPageName.Text);
            }
        }

        public void lbForum_Click(object sender, EventArgs e)
        {
            LinkButton lbForum = sender as LinkButton;
            _presenter.GoToForum(lbForum.Attributes["ForumPageName"]);
        }
    }
}
