using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooCore.Core
{
    public interface IProfileAttributeService
    {
        List<ProfileAttribute> GetProfileAttributesByProfileID(Int32 ProfileID);
    }
}