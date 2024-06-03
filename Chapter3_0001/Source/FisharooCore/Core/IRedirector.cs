using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IRedirector
    {
        void GoToHomePage();
        void GoToErrorPage();

        //CHAPTER 3
        void GoToAccountLoginPage();
        void GoToAccountRegisterPage();
        void GoToAccountEditAccountPage();
        void GoToAccountRecoverPasswordPage();
        void GoToAccountAccessDenied();
    }
}