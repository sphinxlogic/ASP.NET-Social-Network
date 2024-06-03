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
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooWeb.Accounts.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Accounts.Presenter
{
    public class LoginPresenter
    {
        private ILogin _view;
        private IAccountService _accountService;
        private IRedirector _redirector;
        private IWebContext _webContext;

        public void Init(ILogin view)
        {
            _view = view;
            _accountService = ObjectFactory.GetInstance<IAccountService>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();

            if(!string.IsNullOrEmpty(_webContext.FriendshipRequest))
                _view.DisplayMessage("Login to add this friend!");
        }

        public void Login(string username, string password)
        {
            string message = _accountService.Login(username, password);
            _view.DisplayMessage(message);
        }

        public void GoToRegister()
        {
            _redirector.GoToAccountRegisterPage();
        }

        public void GoToRecoverPassword()
        {
            _redirector.GoToAccountRecoverPasswordPage();
        }
    }
}
