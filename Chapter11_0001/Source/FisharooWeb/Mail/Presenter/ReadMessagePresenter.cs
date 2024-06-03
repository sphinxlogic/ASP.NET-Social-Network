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
using Fisharoo.FisharooWeb.Mail.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Mail.Presenter
{
    public class ReadMessagePresenter
    {
        private IReadMessage _view;
        private IWebContext _webContext;
        private IMessageRepository _messageRepository;
        private IUserSession _userSession;
        private IRedirector _redirector;
        public ReadMessagePresenter()
        {
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _messageRepository = ObjectFactory.GetInstance<IMessageRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IReadMessage view)
        {
            _view = view;
            _view.LoadMessage(_messageRepository.GetMessageByMessageID(_webContext.MessageID,_userSession.CurrentUser.AccountID));
        }

        public void Reply()
        {
            _redirector.GoToMailNewMessage(_webContext.MessageID);
        }
    }
}
