using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles.Presenter
{
    public class ManageProfilePresenter
    {
        private IManageProfile _view;
        private IProfileRepository _profileRepository;
        private ILevelOfExperienceTypeRepository _levelOfExperienceTypeRepository;
        private IProfileAttributeRepository _profileAttributeRepository;
        private IUserSession _userSession;
        private IProfileService _profileService;
        private IRedirector _redirector;

        public ManageProfilePresenter()
        {
            _levelOfExperienceTypeRepository = ObjectFactory.GetInstance<ILevelOfExperienceTypeRepository>();
            _profileRepository = ObjectFactory.GetInstance<IProfileRepository>();
            _profileAttributeRepository = ObjectFactory.GetInstance<IProfileAttributeRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _profileService = ObjectFactory.GetInstance<IProfileService>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }

        public void Init(IManageProfile view, bool IsPostback)
        {
            _view = view;

            _view.LoadLevelOfExperienceTypes(_levelOfExperienceTypeRepository.GetAllLevelOfExperienceTypes());
            _view.LoadProfileAttributeTypes(_profileAttributeRepository.GetProfileAttributeTypes());
        }

        public void LoadProfile(bool IsPostback)
        {
            if(!IsPostback)
            {
                Profile profile = _profileService.LoadProfileByAccountID(_userSession.CurrentUser.AccountID);
                _view.LoadProfile(profile);
            }
        }

        public Profile GetProfile()
        {
            return _profileRepository.GetProfileByAccountID(_userSession.CurrentUser.AccountID);
        }

        public Account GetAccount()
        {
            return _userSession.CurrentUser;
        }

        public void SaveProfile(Profile profile)
        {
            _profileService.SaveProfile(profile);
            _redirector.GoToProfilesProfile();
        }

        public List<ProfileAttributeType> GetProfileAttributeTypes()
        {
            return _profileAttributeRepository.GetProfileAttributeTypes();
        }
    }
}
