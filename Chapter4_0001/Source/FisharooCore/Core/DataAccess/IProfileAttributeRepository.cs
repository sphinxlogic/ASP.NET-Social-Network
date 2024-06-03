using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IProfileAttributeRepository
    {
        List<ProfileAttributeType> GetProfileAttributeTypes();
        void AddProfileAttributes(params ProfileAttribute[] attributes);
        void SaveProfileAttribute(ProfileAttribute attribute);
        List<ProfileAttribute> GetProfileAttributesByProfileID(Int32 ProfileID);
        ProfileAttributeType GetProfileAttributeTypeByID(Int32 profileAttributeTypeID);
    }
}