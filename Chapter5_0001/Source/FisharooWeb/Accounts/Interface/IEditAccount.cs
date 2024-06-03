using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Accounts.Interface
{
    public interface IEditAccount
    {
        void ShowMessage(string Message);
        void LoadCurrentInformation(Account account);
    }
}