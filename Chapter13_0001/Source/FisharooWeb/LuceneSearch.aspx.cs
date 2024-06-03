using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooWeb
{
    public partial class LuceneSearch : System.Web.UI.Page
    {
        private ILuceneSearchService _luceneSearchService;
        protected void Page_Load(object sender, EventArgs e)
        {
            _luceneSearchService = ObjectFactory.GetInstance<ILuceneSearchService>();
            _luceneSearchService.RecordAddedEvent += new EventHandler(_luceneSearchService_RecordAddedEvent);
        }

        void _luceneSearchService_RecordAddedEvent(object sender, EventArgs e)
        {
            phResults.Controls.Add(new LiteralControl("<BR>Record added"));
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            phResults.Controls.Clear();
            _luceneSearchService.BuildIndexesThread();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            phResults.Controls.Clear();
            foreach(SearchResult result in _luceneSearchService.Search(txtSearch.Text))
            {
                phResults.Controls.Add(new LiteralControl("<BR>" + result.DisplayText + " " + result.Content));
            }
            
        }
    }
}
