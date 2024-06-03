using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class GagRepository : IGagRepository
    {
        private Connection conn;
        public GagRepository()
        {
            conn = new Connection();
        }

        public bool IsGagged(Int32 AccountID)
        {
            bool result = false;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(dc.Gags.Where(g=>g.AccountID == AccountID && g.GagUntilDate > DateTime.Now).FirstOrDefault() != null)
                {
                    result = true;
                }
            }
            return true;
        }

        public List<Gag> GetActiveGags()
        {
            List<Gag> result = new List<Gag>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Gags.Where(g => g.GagUntilDate > DateTime.Now).OrderBy(g => g.GagUntilDate).ToList();
            }
            return result;
        }

        public Gag SaveGag(Gag gag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(gag.GagID > 0)
                {
                    dc.Gags.Attach(gag, true);
                }
                else
                {
                    dc.Gags.InsertOnSubmit(gag);
                }
                dc.SubmitChanges();
            }
            return gag;
        }

        public void DeleteGag(Gag gag)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Gags.Attach(gag, true);
                dc.Gags.DeleteOnSubmit(gag);
                dc.SubmitChanges();
            }
        }
    }
}
