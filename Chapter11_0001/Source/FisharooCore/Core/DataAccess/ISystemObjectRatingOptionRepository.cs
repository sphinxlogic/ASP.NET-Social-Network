using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ISystemObjectRatingOptionsRepository
    {
        List<SystemObjectRatingOption> GetSystemObjectRatingOptionsBySystemObjectID(int SystemObjectID);
        int SaveSystemObjectRatingOption(SystemObjectRatingOption systemObjectRatingOption);
        void DeleteSystemObjectRatingOption(SystemObjectRatingOption systemObjectRatingOption);
    }
}