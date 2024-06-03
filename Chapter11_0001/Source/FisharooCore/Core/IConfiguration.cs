using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IConfiguration
    {
        string SiteName { get; }
        string RootURL { get; }
        int NumberOfRecordsInPage { get; }
    }
}