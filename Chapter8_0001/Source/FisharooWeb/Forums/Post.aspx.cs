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
using Fisharoo.FisharooWeb.Forums.Interface;
using Fisharoo.FisharooWeb.Forums.Presetner;

namespace Fisharoo.FisharooWeb.Forums
{
    public partial class Post : System.Web.UI.Page, IPost
    {
        private PostPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new PostPresenter();
            _presenter.Init(this);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            BoardPost post = new BoardPost();
            post.Name = txtName.Text;
            post.PageName = txtPageName.Text;
            post.Post = txtPost.Text;
            _presenter.Save(post);
        }

        public void SetDisplay(bool IsThread)
        {
            txtPageName.Enabled = IsThread;
        }
    }
}