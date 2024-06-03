using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class ContentFilterRepository : IContentFilterRepository
    {
        private Connection _conn;
        public ContentFilterRepository()
        {
            _conn = new Connection();
        }

        public List<ContentFilter> GetContentFilters()
        {
            List<ContentFilter> filters = new List<ContentFilter>();
            using (FisharooDataContext dc = _conn.GetContext())
            {
                filters = dc.ContentFilters.ToList();
            }
            return filters;
        }
    }
}
