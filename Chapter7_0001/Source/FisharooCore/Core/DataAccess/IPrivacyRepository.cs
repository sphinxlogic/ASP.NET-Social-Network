using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IPrivacyRepository
    {
        List<PrivacyFlagType> GetPrivacyFlagTypes();
        List<VisibilityLevel> GetVisibilityLevels();
        List<PrivacyFlag> GetPrivacyFlagsByProfileID(Int32 ProfileID);
        void SavePrivacyFlag(PrivacyFlag privacyFlag);
    }
}