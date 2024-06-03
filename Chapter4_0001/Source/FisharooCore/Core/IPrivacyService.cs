using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    //CHAPTER 4
    [PluginFamily("Default")]
    public interface IPrivacyService
    {
        bool ShouldShow(Int32 PrivacyFlagTypeID,
                        Account AccountBeingViewed,
                        Account Account,
                        List<PrivacyFlag> Flags);
    }
}