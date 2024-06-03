using System.Web;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Redirector : IRedirector
    {
        //CHAPTER 3
        public void GoToAccountAccessDenied()
        {
            Redirect("~/Account/AccessDenied.aspx");    
        }
        
        //CHAPTER 3
        public void GoToAccountRecoverPasswordPage()
        {
            Redirect("~/Account/RecoverPassword.aspx");
        }

        //CHAPTER 3
        public void GoToAccountEditAccountPage()
        {
            Redirect("~/Account/EditAccount.aspx");    
        }

        //CHAPTER 3
        public void GoToAccountLoginPage()
        {
            Redirect("~/Account/Login.aspx");
        }

        //CHAPTER 3
        public void GoToAccountRegisterPage()
        {
            Redirect("~/Account/Register.aspx");
        }

        public void GoToHomePage()
        {
            Redirect("~/Default.aspx");
        }

        public void GoToErrorPage()
        {
            Redirect("~/Error.aspx");
        }

        private void Redirect(string path)
        {
            HttpContext.Current.Response.Redirect(path);
        }
    }
}
