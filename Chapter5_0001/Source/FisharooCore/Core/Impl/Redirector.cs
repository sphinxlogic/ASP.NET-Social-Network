using System;
using System.Web;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Redirector : IRedirector
    {
        public void GoToProfilesProfile()
        {
            Redirect("~/Profiles/Profile.aspx");
        }
        public void GoToProfilesDefault()
        {
            Redirect("~/Profiles/Default.aspx");
        }
        public void GoToProfilesManageProfile()
        {
            Redirect("~/Profiles/ManageProfile.aspx");
        }
        public void GoToAccountAccessDenied()
        {
            Redirect("~/Accounts/AccessDenied.aspx");    
        }
        public void GoToAccountRecoverPasswordPage()
        {
            Redirect("~/Accounts/RecoverPassword.aspx");
        }
        public void GoToAccountEditAccountPage()
        {
            Redirect("~/Accounts/EditAccount.aspx");    
        }

        //CHAPTER 5
        public void GoToProfilesStatusUpdates()
        {
            Redirect("~/Profiles/StatusUpdates.aspx");
        }

        //CHAPTER 5
        public void GoToProfilesStatusUpdates(Int32 AccountID)
        {
            Redirect("~/Profiles/StatusUpdates.aspx?" + AccountID.ToString());
        }

        //CHAPTER 5
        public void GoToSearch(string SearchText)
        {
           Redirect("~/Search.aspx?s=" + SearchText); 
        }

        public void GoToAccountLoginPage(string FriendInvitationKey)
        {
            Redirect("~/Accounts/Login.aspx?InvitationKey=" + FriendInvitationKey);
        }
        
        public void GoToAccountLoginPage()
        {
            Redirect("~/Accounts/Login.aspx");
        }
        
        public void GoToFriendsInviteFriends(Int32 AccoundIdToInvite)
        {
            Redirect("~/Friends/InviteFriends.aspx?AccountIdToInvite=" + AccoundIdToInvite.ToString());
        }

        //ChAPTER 5
        public void GoToAccountRegisterPage(string FriendInvitationKey)
        {
            Redirect("~/Accounts/Register.aspx?InvitationKey=" + FriendInvitationKey);
        }

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
