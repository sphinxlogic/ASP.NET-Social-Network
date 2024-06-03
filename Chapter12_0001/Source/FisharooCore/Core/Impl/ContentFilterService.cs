using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class ContentFilterService : IContentFilterService
    {
        
        public ContentFilterService()
        {
            
        }

        public static string Filter(string StringToFilter)
        {
            IContentFilterRepository _contentFilterRepository = ObjectFactory.GetInstance<IContentFilterRepository>();
            List<ContentFilter> _contentFilters = _contentFilterRepository.GetContentFilters();

            StringBuilder sb = new StringBuilder(StringToFilter);

            //encode the final output for further security
            sb = new StringBuilder(HttpUtility.HtmlEncode(sb.ToString()));

            //replace all the dirty words and forbidden tags
            foreach (ContentFilter cf in _contentFilters)
            {
                sb.Replace(cf.StringToFilter, cf.ReplaceWith);
            }

            return sb.ToString();
        }
    }
}
