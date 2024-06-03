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
using Fisharoo.FisharooWeb.Account.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Account.Presenter
{
    public class LoginPresenter
    {
        private ILogin _view;
        private IAccountService _accountService;
        private IRedirector _redirector;

        public void Init(ILogin view)
        {
            _view = view;
            _accountService = ObjectFactory.GetInstance<IAccountService>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
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
