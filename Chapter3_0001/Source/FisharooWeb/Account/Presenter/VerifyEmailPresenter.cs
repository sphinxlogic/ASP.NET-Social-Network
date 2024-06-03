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
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooWeb.Account.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Account.Presenter
{
    public class VerifyEmailPresenter
    {
        private IWebContext _webContext;
        private IAccountRepository _accountRepository;
        public void Init(IVerifyEmail _view)
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();

            string username = Cryptography.Decrypt(_webContext.UsernameToVerify, "verify");

            FisharooCore.Core.Domain.Account account = _accountRepository.GetAccountByUsername(username);

            if(account != null)
            {
                account.EmailVerified = true;
                _accountRepository.SaveAccount(account);
                _view.ShowMessage("Your email address has been successfully verified!");
            }
            else
            {
                _view.ShowMessage("There appears to be something wrong with your verification link!  Please try again.  If you are having issues by clicking on the link, please try copying the URL from your email and pasting it into your browser window.");
            }
        }
    }
}
