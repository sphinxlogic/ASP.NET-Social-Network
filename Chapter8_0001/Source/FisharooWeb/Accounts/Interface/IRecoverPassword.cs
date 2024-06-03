namespace Fisharoo.FisharooWeb.Accounts.Interface
{
    public interface IRecoverPassword
    {
        void ShowMessage(string Message);
        void ShowRecoverPasswordPanel(bool Value);
    }
}