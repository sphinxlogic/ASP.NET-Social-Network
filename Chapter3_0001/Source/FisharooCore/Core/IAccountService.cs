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
    }
}