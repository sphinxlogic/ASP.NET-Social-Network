using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IProfileAttributeService
    {
        List<ProfileAttribute> GetProfileAttributesByProfileID(Int32 ProfileID);
    }
}