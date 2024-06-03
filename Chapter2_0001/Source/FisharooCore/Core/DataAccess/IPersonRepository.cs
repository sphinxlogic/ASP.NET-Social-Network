using System.Collections.Generic;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IPersonRepository
    {
        List<string> GetAllNames();
    }
}