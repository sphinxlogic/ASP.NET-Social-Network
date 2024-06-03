using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    //CHAPTER 3
    public class LogInLinksPresenter
    {
        private IUserSession _userSession;
        private IRedirector _redirector;
        private IAccountService _accountService;

        public void Init(ILogInLinks _view)
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _accountService = ObjectFactory.GetInstance<IAccountService>();

            _view.ShowAppropriateLoginStatePanel(_userSession.LoggedIn,_userSession.Username);
        }

        public void LogOut()
        {
            _accountService.Logout();
        }

        public void LogIn()
        {
            _redirector.GoToAccountLoginPage();
        }

        public void Register()
        {
            _redirector.GoToAccountRegisterPage();
        }

        public void Home()
        {
            _redirector.GoToHomePage();
        }

        public void EditAccount()
        {
            _redirector.GoToAccountEditAccountPage();
        }
    }
}