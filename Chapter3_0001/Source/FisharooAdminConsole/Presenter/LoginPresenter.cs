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
using Fisharoo.FisharooAdminConsole.Interface;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooAdminConsole.Presenter
{
    public class LoginPresenter
    {
        private ILogin _view;
        private IAccountService _accountService;

        public void Init(ILogin view)
        {
            _view = view;
            _accountService = ObjectFactory.GetInstance<IAccountService>();
        }

        public void LogIn(string Username, string Password)
        {
            _view.ShowMessage(_accountService.Login(Username, Cryptography.Encrypt(Password, Username)));
        }
    }
}
