namespace Fisharoo.FisharooWeb.Account.Interface
{
    public interface IEditAccount
    {
        void ShowMessage(string Message);
        void LoadCurrentInformation(FisharooCore.Core.Domain.Account account);
    }
}