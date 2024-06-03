using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IGagRepository
    {
        List<Gag> GetActiveGags();
        Gag SaveGag(Gag gag);
        void DeleteGag(Gag gag);
        bool IsGagged(Int32 AccountID);

    }
}