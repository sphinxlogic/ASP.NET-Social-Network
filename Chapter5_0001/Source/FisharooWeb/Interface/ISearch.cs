using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Interface
{
    public interface ISearch
    {
        void LoadAccounts(List<Account> Accounts);
    }
}