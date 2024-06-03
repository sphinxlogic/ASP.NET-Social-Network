using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    public class CommentsPresenter
    {
        private IComments _view;
        private ICommentRepository _commentRepository;
        private IWebContext _webContext;

        public CommentsPresenter()
        {
            _commentRepository = ObjectFactory.GetInstance<ICommentRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IComments view, bool IsPostBack)
        {
            _view = view;

            if(_webContext.CurrentUser != null)
                _view.ShowCommentBox(true);
            else
                _view.ShowCommentBox(false);
        }

        public void LoadComments()
        {
            _view.LoadComments(_commentRepository.GetCommentsBySystemObject(_view.SystemObjectID,
                                                                             _view.SystemObjectRecordID));    
        }

        public void AddComment(string comment)
        {
            Comment c = new Comment();
            c.Body = comment;
            c.CommentByAccountID = _webContext.CurrentUser.AccountID;
            c.CommentByUsername = _webContext.CurrentUser.Username;
            c.CreateDate = DateTime.Now;
            c.SystemObjectID = _view.SystemObjectID;
            c.SystemObjectRecordID = _view.SystemObjectRecordID;
            _commentRepository.SaveComment(c);
            _view.ClearComments();
            LoadComments();
        }
    }
}
