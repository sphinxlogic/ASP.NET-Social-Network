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
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using Fisharoo.FisharooWeb.UserControls.Presenters;

namespace Fisharoo.FisharooWeb.UserControls
{
    public partial class Comments : System.Web.UI.UserControl, IComments
    {
        private CommentsPresenter _presenter;
        public int SystemObjectID { get; set; }
        public long SystemObjectRecordID { get; set; }
        
        protected override void OnInit(EventArgs e)
        {
            _presenter = new CommentsPresenter();
            _presenter.Init(this, IsPostBack);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.LoadComments();
        }

        protected void btnAddComment_Click(object sender, EventArgs e)
        {
            _presenter.AddComment(txtComment.Text);
            txtComment.Text = "";
        }

        public void ShowCommentBox(bool IsVisible)
        {
            pnlComment.Visible = IsVisible;
        }

        public void ClearComments()
        {
            phComments.Controls.Clear();    
        }

        public void LoadComments(List<Comment> comments)
        {
            if(comments.Count > 0)
            {
                phComments.Controls.Add(new LiteralControl("<table width=\"100%\">"));
                foreach (Comment comment in comments)
                {
                    phComments.Controls.Add(new LiteralControl("<tr><td>" + comment.CommentByUsername + " (" + comment.CreateDate.ToShortDateString() + "): " + comment.Body + "</td></tr>"));
                }
                phComments.Controls.Add(new LiteralControl("</table>"));
            }
        }
    }
}