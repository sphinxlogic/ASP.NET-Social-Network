using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class TermRepository : ITermRepository
    {
        private Connection conn;

        public TermRepository()
        {
            conn = new Connection();
        }

        public Term GetCurrentTerm()
        {
            Term returnTerm = null;

            using (FisharooDataContext dc = conn.GetContext())
            {
                var terms = (from t in dc.Terms
                            orderby t.CreateDate descending
                            select t).Take(1);

                foreach (Term term in terms)
                {
                    returnTerm = term;
                }
            }

            return returnTerm;
        }

        public void SaveTerm(Term term)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (term.TermID > 0)
                {
                    dc.Terms.Attach(term);
                }
                else
                {
                    dc.Terms.InsertOnSubmit(term);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteTerm(Term term)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Terms.Attach(term, true);
                dc.Terms.DeleteOnSubmit(term);
                dc.SubmitChanges();
            }
        }
    }
}
