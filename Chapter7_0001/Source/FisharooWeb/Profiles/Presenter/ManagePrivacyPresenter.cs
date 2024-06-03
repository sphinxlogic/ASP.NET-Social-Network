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
    public class ManagePrivacyPresenter
    {
        private IPrivacyRepository _privacyRepository;
        private IProfileService _profileService;
        private Profile profile;
        private IUserSession _userSession;
        private Account account;

        private List<PrivacyFlagType> privacyFlagTypes;
        private List<VisibilityLevel> visibilityLevels;
        private List<PrivacyFlag> privacyFlags;
        private IManagePrivacy _view;

        public ManagePrivacyPresenter()
        {
            _privacyRepository = ObjectFactory.GetInstance<IPrivacyRepository>();
            _profileService = ObjectFactory.GetInstance<IProfileService>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();

            account = _userSession.CurrentUser;
            profile = _profileService.LoadProfileByAccountID(account.AccountID);
        }

        public void Init(IManagePrivacy View)
        {
            _view = View;

            LoadPrivacyTypes();
        }

        private void LoadPrivacyTypes()
        {
            privacyFlagTypes = _privacyRepository.GetPrivacyFlagTypes();
            visibilityLevels = _privacyRepository.GetVisibilityLevels();
            privacyFlags = _privacyRepository.GetPrivacyFlagsByProfileID(profile.ProfileID);

            _view.ShowPrivacyTypes(privacyFlagTypes,visibilityLevels,privacyFlags);
        }

        public List<PrivacyFlagType> GetPrivacyFlagTypes()
        {
            return privacyFlagTypes;    
        }

        public void SavePrivacyFlag(Int32 PrivacyFlagTypeID, Int32 VisibilityLevelID)
        {
            foreach (PrivacyFlag flag in privacyFlags)
            {
                if (flag.PrivacyFlagTypeID == PrivacyFlagTypeID)
                {
                    flag.VisibilityLevelID = VisibilityLevelID;
                    _privacyRepository.SavePrivacyFlag(flag);
                    return;
                }
            }

            //not in collection?  Add a new one
            PrivacyFlag newFlag = new PrivacyFlag();
            newFlag.PrivacyFlagTypeID = PrivacyFlagTypeID;
            newFlag.VisibilityLevelID = VisibilityLevelID;
            newFlag.ProfileID = profile.ProfileID;
            newFlag.CreateDate = DateTime.Now;
            privacyFlags.Add(newFlag);
            _privacyRepository.SavePrivacyFlag(newFlag);
        }

        public bool IsFlagSelected(Int32 PrivacyFlagTypeID, Int32 VisibilityLevelID, List<PrivacyFlag> PrivacyFlags)
        {
            List<PrivacyFlag> result = PrivacyFlags.Where(pf => pf.PrivacyFlagTypeID == PrivacyFlagTypeID && pf.VisibilityLevelID == VisibilityLevelID).ToList();
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
