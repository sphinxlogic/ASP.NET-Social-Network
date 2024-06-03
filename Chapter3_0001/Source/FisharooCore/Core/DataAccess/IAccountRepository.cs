using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IAccountRepository
    {
        Account GetAccountByID(int AccountID);
        void SaveAccount(Account account);
        Account GetAccountByEmail(string Email);
        Account GetAccountByUsername(string Username);
        void AddPermission(Account account, Permission permission);
        List<Account> GetAllAccounts(Int32 PageNumber);
    }
}