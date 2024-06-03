using System.Web;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Redirector : IRedirector
    {
        //CHAPTER 4
        public void GoToProfilesProfile()
        {
            Redirect("~/Profiles/Profile.aspx");
        }

        //CHAPTER 4
        public void GoToProfilesDefault()
        {
            Redirect("~/Profiles/Default.aspx");
        }

        //CHAPTER 4
        public void GoToProfilesManageProfile()
        {
            Redirect("~/Profiles/ManageProfile.aspx");
        }

        //CHAPTER 3
        public void GoToAccountAccessDenied()
        {
            Redirect("~/Accounts/AccessDenied.aspx");    
        }
        
        //CHAPTER 3
        public void GoToAccountRecoverPasswordPage()
        {
            Redirect("~/Accounts/RecoverPassword.aspx");
        }

        //CHAPTER 3
        public void GoToAccountEditAccountPage()
        {
            Redirect("~/Accounts/EditAccount.aspx");    
        }

        //CHAPTER 3
        public void GoToAccountLoginPage()
        {
            Redirect("~/Accounts/Login.aspx");
        }

        //CHAPTER 3
        public void GoToAccountRegisterPage()
        {
            Redirect("~/Accounts/Register.aspx");
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
