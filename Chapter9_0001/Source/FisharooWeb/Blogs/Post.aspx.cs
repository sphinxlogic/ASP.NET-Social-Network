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
using Fisharoo.FisharooWeb.Blogs.Interface;
using Fisharoo.FisharooWeb.Blogs.Presenter;

namespace Fisharoo.FisharooWeb.Blogs
{
    public partial class Post : System.Web.UI.Page, IPost 
    {
        private PostPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new PostPresenter();
            _presenter.Init(this);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Blog blog = new Blog();
            if (litBlogID.Text != "")
                blog.BlogID = Convert.ToInt64(litBlogID.Text);
            blog.IsPublished = chkIsPublished.Checked;
            blog.PageName = txtPageName.Text;
            blog.Post = txtPost.Text;
            blog.Subject = txtSubject.Text;
            blog.Title = txtTitle.Text;
            _presenter.SavePost(blog);
        }

        public void LoadPost(Blog blog)
        {
            txtTitle.Text = blog.Title;
            txtSubject.Text = blog.Subject;
            txtPost.Text = blog.Post;
            txtPageName.Text = blog.PageName;
            chkIsPublished.Checked = blog.IsPublished;
            litBlogID.Text = blog.BlogID.ToString();
        }

        public void ShowError(string ErrorMessage)
        {
            lblErrorMessage.Text = ErrorMessage;
        }
    }
}
