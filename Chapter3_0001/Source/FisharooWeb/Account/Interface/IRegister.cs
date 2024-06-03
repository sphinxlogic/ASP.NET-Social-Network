using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Account
{
    public interface IRegister
    {
        void ShowErrorMessage(string Message);
        void ShowAccountCreatedPanel();
        void ShowCreateAccountPanel();
        void ToggleWizardIndex(int index);
        void LoadTerms(Term term);
    }
}