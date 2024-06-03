using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class ModerationRepository : IModerationRepository
    {
        private Connection conn;
        private IConfiguration _configuration;
        public ModerationRepository()
        {
            conn = new Connection();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();
        }

        public bool HasFlaggedThisAlready(int AccountID, int SystemObjectID, long SystemObjectRecordID)
        {
            bool result = false;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (dc.Moderations.Where(m => m.AccountID == AccountID && 
                    m.SystemObjectID == SystemObjectID && 
                    m.SystemObjectRecordID == SystemObjectRecordID)
                    .FirstOrDefault() != null)
                    result = true;
            }
            return result;
        }

        public List<Moderation> GetModerationsByAccountID(int AccountID)
        {
            List<Moderation> result = new List<Moderation>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                var groups = (from m in dc.Moderations
                              where m.AccountID == AccountID && (m.IsDenied == null || m.IsApproved == null)
                              group m by m.SystemObjectRecordID
                              into g
                                  select new { g, NumberOfReports = g.Count() }).OrderByDescending(g1 => g1.NumberOfReports);
                foreach (var v in groups)
                {
                    result.Add(v.g.ToList()[0]);
                }
            }
            return result;
        }

        public List<Moderation> GetModerationsGlobal()
        {
            List<Moderation> result = new List<Moderation>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                var groups = (from m in dc.Moderations
                              where m.IsDenied == null || m.IsApproved == null
                              group m by m.SystemObjectRecordID
                              into g
                                  select new { g, NumberOfReports = g.Count() }).OrderByDescending(g1 => g1.NumberOfReports);

                foreach (var v in groups)
                {
                    result.Add(v.g.ToList()[0]);
                }
            }
            return result;
        }

        public void SaveModerationResults(List<ModerationResult> results, int ActionByAccountID, string ActionByUsername)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                foreach (ModerationResult result in results)
                {
                    List<Moderation> moderations =
                        dc.Moderations.Where(
                            m =>
                            m.SystemObjectID == result.SystemObjectID &&
                            m.SystemObjectRecordID == result.SystemObjectRecordID).ToList();

                    for (int i = 0; i < moderations.Count(); i++)
                    {
                        if (result.IsApproved)
                        {
                            moderations[i].IsApproved = true;
                            moderations[i].IsDenied = false;
                        }
                        else
                        {
                            moderations[i].IsDenied = true;
                            moderations[i].IsApproved = false;
                        }
                        moderations[i].ActionByAccountID = ActionByAccountID;
                        moderations[i].ActionByUsername = ActionByUsername;
                    }

                    if(moderations.Count() > 0)
                        dc.SubmitChanges();
                }
            }
        }

        public Moderation SaveModeration(Moderation moderation)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(moderation.ModerationID > 0)
                {
                    dc.Moderations.Attach(moderation, true);
                }
                else
                {
                    dc.Moderations.InsertOnSubmit(moderation);
                }
                dc.SubmitChanges();
            }
            return moderation;
        }

        public void DeleteModeration(Moderation moderation)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Moderations.Attach(moderation, true);
                dc.Moderations.DeleteOnSubmit(moderation);
                dc.SubmitChanges();
            }
        }
    }

    public struct ModerationResult
    {
        public int SystemObjectID { get; set; }
        public long SystemObjectRecordID { get; set; }
        public bool IsApproved { get; set; }
    }
}
