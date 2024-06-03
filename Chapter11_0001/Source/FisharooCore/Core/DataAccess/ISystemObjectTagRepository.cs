using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ISystemObjectTagRepository
    {
        long SaveSystemObjectTag(SystemObjectTag tag);
        void DeleteSystemObjectTag(SystemObjectTag tag);
        List<SystemObjectTagWithObject> GetSystemObjectsByTagID(int TagID);
    }
}