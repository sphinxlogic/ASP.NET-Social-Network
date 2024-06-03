using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IProfileRepository
    {
        Profile GetProfileByAccountID(int AccountID);
        Int32 SaveProfile(Profile profile);
        void DeleteProfile(Profile profile);
        List<Profile> GetProfilesForIndexing(int PageNumber);
    }
}