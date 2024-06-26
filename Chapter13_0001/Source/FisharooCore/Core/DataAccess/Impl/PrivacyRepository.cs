﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    //CHAPTER 4
    [Pluggable("Default")]
    public class PrivacyRepository : IPrivacyRepository
    {
        private Connection conn;
        public PrivacyRepository()
        {
            conn = new Connection();
        }

        public List<PrivacyFlagType> GetPrivacyFlagTypes()
        {
            List<PrivacyFlagType> result = new List<PrivacyFlagType>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<PrivacyFlagType> query = dc.PrivacyFlagTypes.OrderBy(pft => pft.SortOrder);
                result = query.ToList();
            }
            return result;
        }

        public List<VisibilityLevel> GetVisibilityLevels()
        {
            List<VisibilityLevel> result = new List<VisibilityLevel>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<VisibilityLevel> query = dc.VisibilityLevels;
                result = query.ToList();
            }
            return result;
        }

        public List<PrivacyFlag> GetPrivacyFlagsByProfileID(Int32 ProfileID)
        {
            List<PrivacyFlag> result = new List<PrivacyFlag>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<PrivacyFlag> query = dc.PrivacyFlags.Where(pf => pf.ProfileID == ProfileID);
                result = query.ToList();
            }
            return result;
        }

        public void SavePrivacyFlag(PrivacyFlag privacyFlag)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (privacyFlag.PrivacyFlagID > 0)
                {
                    dc.PrivacyFlags.Attach(privacyFlag, true);
                }
                else
                {
                    dc.PrivacyFlags.InsertOnSubmit(privacyFlag);
                }
                dc.SubmitChanges();
            }
        }
    }
}
