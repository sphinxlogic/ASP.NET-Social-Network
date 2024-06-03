using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IProfileService
    {
        Profile LoadProfileByAccountID(Int32 AccountID);
        void SaveProfile(Profile profile);
    }
}