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
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooWeb.Accounts.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Accounts.Presenter
{
    public class RecoverPasswordPresenter
    {
        private IRecoverPassword _view;
        private IEmail _email;
        private IAccountRepository _accountRepository;

        public RecoverPasswordPresenter()
        {
            _email = ObjectFactory.GetInstance<IEmail>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
        }

        public void Init(IRecoverPassword View)
        {
            _view = View;
        }

        public void RecoverPassword(string Email)
        {
            Account account = _accountRepository.GetAccountByEmail(Email);

            if(account != null)
            {
                _email.SendPasswordReminderEmail(account.Email, account.Password, account.Username);
                _view.ShowRecoverPasswordPanel(false);
                _view.ShowMessage("An email was sent to your account!");
            }
            else
            {
                _view.ShowRecoverPasswordPanel(true);
                _view.ShowMessage("We couldn't find the account you requested.");
            }
            
        }
    }
}
