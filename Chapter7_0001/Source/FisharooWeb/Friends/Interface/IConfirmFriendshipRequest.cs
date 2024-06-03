using System;

namespace Fisharoo.FisharooWeb.Friends.Interface
{
    public interface IConfirmFriendshipRequest
    {
        void LoadDisplay(string FriendInvitationKey, Int32 AccountID, string AccountFirstName, string AccountLastName, string SiteName);
        void ShowConfirmPanel(bool value);
        void ShowMessage(string Message);
    }
}