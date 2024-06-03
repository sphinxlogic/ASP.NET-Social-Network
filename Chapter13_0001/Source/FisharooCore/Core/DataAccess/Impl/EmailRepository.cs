using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class EmailRepository : IEmailRepository
    {
        private Connection conn;
        public EmailRepository()
        {
            conn = new Connection();
        }

        public void Save(MailQueue_Receiving MailQueue)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                dc.MailQueue_Receivings.InsertOnSubmit(MailQueue);
                dc.SubmitChanges();
            }
        }

        public List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult> GetMailQueueToProcess()
        {
            List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult> results = new List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult>();
            using (FisharooDataContext dc = conn.GetContext())
            {
                results = dc.pr_MailQueue_SwapReceivingAndWorking_GetWorking().ToList();
            }
            return results;
        }

        public void MoveMailQueueWorkingToHistory()
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                dc.pr_MailQueue_MoveWorkingToHistory();
            }
        }
    }
}
