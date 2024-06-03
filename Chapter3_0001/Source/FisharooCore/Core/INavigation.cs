using System.Collections.Generic;
using System.Web;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface INavigation
    {
        SiteMapNode CurrentNode { get; }
        SiteMapNode RootNode { get; }
        List<SiteMapNode> PrimaryNodes();
        List<SiteMapNode> FooterNodes();
        void CheckAccessForCurrentNode();
        List<SiteMapNode> AllNodes();
    }
}