using System;
using System.Collections.Generic;
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
using Fisharoo.FisharooWeb.Mail.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Mail.Presenter
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IMessageRepository _messageRepository;
        private IMessageRecipientRepository _messageRecipientRepository;
        private IUserSession _userSession;
        private IWebContext _webContext;

        public DefaultPresenter()
        {
            _messageRepository = ObjectFactory.GetInstance<IMessageRepository>();
            _messageRecipientRepository = ObjectFactory.GetInstance<IMessageRecipientRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IDefault view)
        {
            _view = view;
            if (_userSession.CurrentUser != null)
            {
                _view.LoadMessages(_messageRepository.GetMessagesByAccountID(_userSession.CurrentUser.AccountID,
                                                                             _webContext.PageNumber,
                                                                             (MessageFolders) _webContext.FolderID));
                _view.DisplayPageNavigation(
                    _messageRepository.GetPageCount((MessageFolders) _webContext.FolderID,
                                                    _userSession.CurrentUser.AccountID),
                    (MessageFolders) _webContext.FolderID, _webContext.PageNumber);
            }
        }

        public void DeleteSelected()
        {
            List<Int32> messages = _view.ExtractSelectedMessages();
            foreach (int i in messages)
            {
                MessageWithRecipient m = _messageRepository.GetMessageByMessageID(i, _userSession.CurrentUser.AccountID);
                _messageRecipientRepository.DeleteMessageRecipient(m.MessageRecipient);
            }
            HttpContext.Current.Response.Redirect("~/mail/default.aspx?folder=" + _webContext.FolderID + "&page=" + _webContext.PageNumber);
        }

        public void MarkSelectedAsUnread()
        {
            List<Int32> messages = _view.ExtractSelectedMessages();
        }
    }
}
