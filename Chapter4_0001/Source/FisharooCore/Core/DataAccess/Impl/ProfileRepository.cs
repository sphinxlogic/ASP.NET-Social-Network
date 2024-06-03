using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")] 
    public class ProfileRepository : IProfileRepository
    {
        private Connection conn;
        private IAlertService _alertService;

        public ProfileRepository()
        {
            conn = new Connection();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
        }

        public Profile GetProfileByAccountID(int AccountID)
        {
            Profile profile;

            using (FisharooDataContext dc = conn.GetContext())
            {
                profile = (from p in dc.Profiles
                           where p.AccountID == AccountID
                           select p).FirstOrDefault();
            }

            return profile;
        }

        public Int32 SaveProfile(Profile profile)
        {
            Int32 profileID;
            profile.LastUpdateDate = DateTime.Now;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(profile.ProfileID > 0)
                {
                    dc.Profiles.Attach(profile, true);
                    _alertService.AddProfileModifiedAlert();
                }
                else
                {
                    profile.CreateDate = DateTime.Now;
                    dc.Profiles.InsertOnSubmit(profile);
                    _alertService.AddProfileCreatedAlert();
                }
                dc.SubmitChanges();
                profileID = profile.ProfileID;
            }
            return profileID;
        }

        public void DeleteProfile(Profile profile)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Profiles.DeleteOnSubmit(profile);
                dc.SubmitChanges();
            }
        }
    }
}
