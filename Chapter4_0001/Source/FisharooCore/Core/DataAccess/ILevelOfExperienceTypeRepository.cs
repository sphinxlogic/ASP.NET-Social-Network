using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ILevelOfExperienceTypeRepository
    {
        List<LevelOfExperienceType> GetAllLevelOfExperienceTypes();
        LevelOfExperienceType GetLevelOfExperienceTypeByID(int LevelOfExperienceTypeID);
        void SaveLevelOfExperienceType(LevelOfExperienceType levelOfExperienceType);
        void DeleteLevelOfExperienceType(LevelOfExperienceType levelOfExperienceType);
    }
}