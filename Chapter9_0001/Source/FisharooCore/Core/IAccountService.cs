using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IAccountService
    {
        bool UsernameInUse(string Username);
        bool EmailInUse(string Email);
        string Login(string Username, string Password);
        void Logout();
        Account GetAccountByID(Int32 AccountID);
    }
}