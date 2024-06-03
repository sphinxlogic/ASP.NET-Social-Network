using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IWebContext
    {
        void ClearSession();
        bool ContainsInSession(string key);
        void RemoveFromSession(string key);

        //CHAPTER 3
        bool LoggedIn { get; set; }
        string Username { get; set;  }
        Account CurrentUser { get; set; }
        string CaptchaImageText { get; set; }
        string UsernameToVerify { get; }
    }
}