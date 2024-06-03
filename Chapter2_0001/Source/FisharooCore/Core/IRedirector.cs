using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IRedirector
    {
        void GoToHomePage();
        void GoToErrorPage();
    }
}