using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class AlertRepository : IAlertRepository
    {
        private Connection conn;
        public AlertRepository()
        {
            conn = new Connection();
        }

        public List<Alert> GetAlertsByAccountID(Int32 AccountID)
        {
            List<Alert> result = new List<Alert>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<Alert> alerts = (from a in dc.Alerts
                                             where a.AccountID == AccountID
                                             orderby a.CreateDate descending
                                             select a).Take(40);
                result = alerts.ToList();
            }
            return result;
        }

        public void SaveAlert(Alert alert)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(alert.AlertID > 0)
                {
                    dc.Alerts.Attach(alert, true);
                }
                else
                {
                    alert.CreateDate = DateTime.Now;
                    dc.Alerts.InsertOnSubmit(alert);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteAlert(Alert alert)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Alerts.DeleteOnSubmit(alert);
                dc.SubmitChanges();
            }
        }
    }
}
