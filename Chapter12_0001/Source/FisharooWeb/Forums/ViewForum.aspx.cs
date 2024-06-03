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
using Fisharoo.FisharooWeb.Forums.Interface;
using Fisharoo.FisharooWeb.Forums.Presetner;
using StructureMap;

namespace Fisharoo.FisharooWeb.Forums
{
    public partial class Forum : System.Web.UI.Page, IViewForum 
    {
        private ViewForumPresenter _presenter;
        protected IRedirector _redirector;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ViewForumPresenter();
            _presenter.Init(this);
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }

        public void LoadDisplay(List<BoardPost> Threads, string CategoryPageName, string ForumPageName, Int32 ForumID)
        {
            litCategoryPageName.Text = CategoryPageName;
            litForumPageName.Text = ForumPageName;
            linkNewThread.NavigateUrl = "/forums/post.aspx?IsThread=1&ForumID=" + ForumID.ToString();
            repTopics.DataSource = Threads;
            repTopics.DataBind();
        }

        protected void repTopics_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkViewTopic = e.Item.FindControl("linkViewTopic") as HyperLink;
                linkViewTopic.NavigateUrl = "/forums/" + litCategoryPageName.Text + "/" + litForumPageName.Text + "/" +
                                            ((BoardPost) e.Item.DataItem).PageName + ".aspx";
            }
        }
    }
}