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
    public partial class ViewPost : System.Web.UI.Page, IViewPost
    {
        private ViewPostPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ViewPostPresenter();
            _presenter.Init(this);
        }

        public void LoadPost(Blog blog)
        {
            linkProfile.NavigateUrl = "/" + blog.Account.Username;
            lblTitle.Text = blog.Title;
            lblPost.Text = blog.Post;
            imgAvatar.ImageUrl += "?AccountID=" + blog.AccountID.ToString();
            lblCreated.Text = blog.CreateDate.ToString();
            lblUpdated.Text = blog.UpdateDate.ToString();
        }
    }
}
