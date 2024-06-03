using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class ProfileAttributeService : IProfileAttributeService
    {
        private IProfileAttributeRepository profileAttributeRepository;
        public ProfileAttributeService()
        {
            profileAttributeRepository = ObjectFactory.GetInstance<IProfileAttributeRepository>();  
        }

        public List<ProfileAttribute> GetProfileAttributesByProfileID(Int32 ProfileID)
        {
            List<ProfileAttribute> attributes = profileAttributeRepository.GetProfileAttributesByProfileID(ProfileID);
            foreach (ProfileAttribute attribute in attributes)
            {
                attribute.ProfileAttributeType =
                    profileAttributeRepository.GetProfileAttributeTypeByID(attribute.ProfileAttributeTypeID);
            }
            return attributes;
        }       
    }
}
