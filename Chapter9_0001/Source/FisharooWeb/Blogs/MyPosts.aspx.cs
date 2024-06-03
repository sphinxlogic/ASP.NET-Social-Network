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
using Fisharoo.FisharooWeb.Blogs.Interface;
using Fisharoo.FisharooWeb.Blogs.Presenter;

namespace Fisharoo.FisharooWeb.Blogs
{
    public partial class MyPosts : System.Web.UI.Page, IMyPosts 
    {
        private MyPostsPresenter _presenter;
        public MyPosts()
        {
            _presenter = new MyPostsPresenter();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Init(this);
        }

        public void LoadBlogs(List<Blog> Blogs)
        {
            lvBlogs.DataSource = Blogs;
            lvBlogs.DataBind();
        }

        public void lvBlogs_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            Literal litBlogID = e.Item.FindControl("litBlogID") as Literal;
            HyperLink linkTitle = e.Item.FindControl("linkTitle") as HyperLink;
            Literal litPageName = e.Item.FindControl("litPageName") as Literal;
            Literal litUsername = e.Item.FindControl("litUsername") as Literal;
            LinkButton lbEdit = e.Item.FindControl("lbEdit") as LinkButton;
            LinkButton lbDelete = e.Item.FindControl("lbDelete") as LinkButton;

            lbEdit.Attributes.Add("BlogID",litBlogID.Text);
            lbDelete.Attributes.Add("BlogID",litBlogID.Text);

            //linkTitle.NavigateUrl = "~/Blogs/ViewPost.aspx?BlogID=" + litBlogID.Text;
            linkTitle.NavigateUrl = "~/Blogs/" + litUsername.Text + "/" + litPageName.Text + ".aspx";
        }

        public void lbEdit_Click(object sender, EventArgs e)
        {
            LinkButton lbEdit = sender as LinkButton;
            _presenter.EditBlog(Convert.ToInt64(lbEdit.Attributes["BlogID"]));
        }

        public void lbDelete_Click(object sender, EventArgs e)
        {
            LinkButton lbDelete = sender as LinkButton;
            _presenter.DeletedBlog(Convert.ToInt64(lbDelete.Attributes["BlogID"]));
        }
    }
}
