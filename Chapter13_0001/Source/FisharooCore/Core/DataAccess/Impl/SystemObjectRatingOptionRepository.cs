using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class SystemObjectRatingOptionRepository : ISystemObjectRatingOptionRepository
    {
        private Connection conn;
        public SystemObjectRatingOptionRepository()
        {
            conn = new Connection();
        }
        public List<SystemObjectRatingOption> GetSystemObjectRatingOptionsBySystemObjectID(int SystemObjectID)
        {
            List<SystemObjectRatingOption> result = new List<SystemObjectRatingOption>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.SystemObjectRatingOptions.Where(soro => soro.SystemObjectID == SystemObjectID).ToList();
            }
            return result;
        }

        public int SaveSystemObjectRatingOption(SystemObjectRatingOption systemObjectRatingOption)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if(systemObjectRatingOption.SystemObjectRatingOptionID > 0)
                {
                    dc.SystemObjectRatingOptions.Attach(systemObjectRatingOption, true);
                }
                else
                {
                    dc.SystemObjectRatingOptions.InsertOnSubmit(systemObjectRatingOption);
                }
                dc.SubmitChanges();
            }
            return systemObjectRatingOption.SystemObjectRatingOptionID;
        }

        public void DeleteSystemObjectRatingOption(SystemObjectRatingOption systemObjectRatingOption)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.SystemObjectRatingOptions.Attach(systemObjectRatingOption, true);
                dc.SystemObjectRatingOptions.DeleteOnSubmit(systemObjectRatingOption);
                dc.SubmitChanges();
            }
        }
    }
}
