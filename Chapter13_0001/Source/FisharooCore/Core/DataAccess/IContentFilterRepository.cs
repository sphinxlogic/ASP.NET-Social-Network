using System.Collections.Generic;
using StructureMap;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [PluginFamily("Default")]
    public interface IContentFilterRepository
    {
        List<ContentFilter> GetContentFilters();
    }
}