using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    //CHAPTER 5
    [Pluggable("Default")]
    public class StatusUpdateRepository : IStatusUpdateRepository
    {
        private Connection conn;
        public StatusUpdateRepository()
        {
            conn = new Connection();
        }

        public StatusUpdate GetStatusUpdateByID(Int32 StatusUpdateID)
        {
            StatusUpdate result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.StatusUpdates.Where(su => su.StatusUpdateID == StatusUpdateID).FirstOrDefault();
            }
            return result;
        }

        public List<StatusUpdate> GetTopNStatusUpdatesByAccountID(Int32 AccountID, Int32 Number)
        {
            List<StatusUpdate> result = new List<StatusUpdate>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<StatusUpdate> statusUpdates = (from su in dc.StatusUpdates
                                                          where su.AccountID == AccountID
                                                          orderby su.CreateDate descending
                                                          select su).Take(Number);
                result = statusUpdates.ToList();
            }
            return result;
        }

        public List<StatusUpdate> GetStatusUpdatesByAccountID(Int32 AccountID)
        {
            List<StatusUpdate> result = new List<StatusUpdate>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<StatusUpdate> statusUpdates = from su in dc.StatusUpdates
                                                          where su.AccountID == AccountID
                                                          orderby su.CreateDate descending
                                                          select su;
                result = statusUpdates.ToList();
            }
            return result;
        }

        public void SaveStatusUpdate(StatusUpdate statusUpdate)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(statusUpdate.StatusUpdateID > 0)
                {
                    dc.StatusUpdates.Attach(statusUpdate, true);
                }
                else
                {
                    statusUpdate.CreateDate = DateTime.Now;
                    dc.StatusUpdates.InsertOnSubmit(statusUpdate);
                }
                dc.SubmitChanges();
            }
        }
    }
}
