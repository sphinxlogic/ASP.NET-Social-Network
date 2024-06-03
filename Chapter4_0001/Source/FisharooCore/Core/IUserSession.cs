using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    //CHAPTER 3
    [PluginFamily("Default")]
    public interface IUserSession
    {
        bool LoggedIn { get; set; }
        string Username { get; set; }
        Account CurrentUser { get; set; }
    }
}