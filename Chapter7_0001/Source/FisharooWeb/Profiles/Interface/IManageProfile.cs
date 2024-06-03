using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Profiles.Interface
{
    public interface IManageProfile
    {
        void ShowMessage(string Message);
        void LoadLevelOfExperienceTypes(List<LevelOfExperienceType> types);
        void LoadProfileAttributeTypes(List<ProfileAttributeType> types);
        void LoadProfile(Profile profile);
    }
}