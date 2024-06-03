using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class ProfileService : IProfileService
    {
        private IProfileRepository _profileRepository;
        private IProfileAttributeRepository _profileAttributeRepository;
        private IProfileAttributeService _profileAttributeService;
        private ILevelOfExperienceTypeRepository _levelOfExperienceTypeRepository;
        private IUserSession _userSession;
        public ProfileService()
        {
            _profileRepository = ObjectFactory.GetInstance<IProfileRepository>();
            _profileAttributeRepository = ObjectFactory.GetInstance<IProfileAttributeRepository>();
            _profileAttributeService = ObjectFactory.GetInstance<IProfileAttributeService>();
            _levelOfExperienceTypeRepository = ObjectFactory.GetInstance<ILevelOfExperienceTypeRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public Profile LoadProfileByAccountID(Int32 AccountID)
        {
            Profile profile = _profileRepository.GetProfileByAccountID(AccountID);
            List<ProfileAttribute> attributes = new List<ProfileAttribute>();
            LevelOfExperienceType levelOfExperienceType;
            if (profile != null && profile.ProfileID > 0)
            {
                attributes = _profileAttributeService.GetProfileAttributesByProfileID(profile.ProfileID);
                levelOfExperienceType =
                    _levelOfExperienceTypeRepository.GetLevelOfExperienceTypeByID(profile.LevelOfExperienceTypeID);
                
                profile.Attributes = attributes;
                profile.LevelOfExperienceType = levelOfExperienceType;
            }
            return profile;
        }

        public void SaveProfile(Profile profile)
        {
            Int32 profileID;
            profileID = _profileRepository.SaveProfile(profile);
            foreach (ProfileAttribute attribute in profile.Attributes)
            {
                attribute.ProfileID = profileID;
                _profileAttributeRepository.SaveProfileAttribute(attribute);
            }

            _userSession.CurrentUser.Profile = LoadProfileByAccountID(_userSession.CurrentUser.AccountID);
        }
    }
}
