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
    public class NewMessagePresenter
    {
        private INewMessage _view;
        private IWebContext _webContext;
        private IMessageService _messageService;
        private IMessageRepository _messageRepository;
        private IUserSession _userSession;
        private IAccountRepository _accountRepository;
        public NewMessagePresenter()
        {
            _messageService = ObjectFactory.GetInstance<IMessageService>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _messageRepository = ObjectFactory.GetInstance<IMessageRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
        }
        public void Init(INewMessage view)
        {
            _view = view;
            if(_webContext.MessageID != 0)
                _view.LoadReply(_messageRepository.GetMessageByMessageID(_webContext.MessageID,_userSession.CurrentUser.AccountID));
            if(_webContext.AccountID != 0)
                _view.LoadTo(_accountRepository.GetAccountByID(_webContext.AccountID).Username);
        }

        public void SendMessage(string Subject, string Message, string[] To)
        {
            _messageService.SendMessage(Message,Subject,To);
        }
    }
}
