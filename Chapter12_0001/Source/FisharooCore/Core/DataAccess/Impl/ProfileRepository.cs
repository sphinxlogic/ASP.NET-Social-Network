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
        private IConfiguration _configuration;

        public ProfileRepository()
        {
            conn = new Connection();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        public List<Profile> GetProfilesForIndexing(int PageNumber)
        {
            List<Profile> results = new List<Profile>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                results = dc.Profiles.Skip(100*(PageNumber - 1)).Take(100).ToList();
            }
            return results;
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
                dc.Profiles.Attach(profile, true);
                dc.Profiles.DeleteOnSubmit(profile);
                dc.SubmitChanges();
            }
        }
    }
}
