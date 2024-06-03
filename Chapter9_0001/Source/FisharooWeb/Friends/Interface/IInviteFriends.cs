namespace Fisharoo.FisharooWeb.Friends.Interface
{
    public interface IInviteFriends
    {
        void DisplayToData(string To);
        void ShowMessage(string Message);
        void ResetUI();
        void TogglePnlInvite(bool IsVisible);
    }
}