using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Interface;
using Fisharoo.FisharooWeb.Presenter;
using Fisharoo.FisharooWeb.UserControls;
using StructureMap;

namespace Fisharoo.FisharooWeb
{
    public partial class Search : System.Web.UI.Page, ISearch 
    {
        private IWebContext _webContext;
        private SearchPresenter _presenter;

        protected override void OnInit(EventArgs e)
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _presenter = new SearchPresenter();
            _presenter.Init(this);


            if (string.IsNullOrEmpty(_webContext.SearchText))
            {
                lblSearchTerm.Text = "Please use the search box to the left!";
            }
            else
            {
                if (!IsPostBack)
                    lblSearchTerm.Text = "You searched for: " + _webContext.SearchText;

                if (_webContext.SearchText.Length > 3)
                    _presenter.PerformSearch(_webContext.SearchText);
                else
                    lblSearchTerm.Text += " <BR><BR> Your search must contain more than 3 characters!";
            }
        }

        public void LoadAccounts(List<Account> Accounts)
        {
            repAccounts.DataSource = Accounts;
            repAccounts.DataBind();
        }

        protected void repAccounts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProfileDisplay pd = e.Item.FindControl("pdProfileDisplay") as ProfileDisplay;
                pd.LoadDisplay((Account)e.Item.DataItem);
                if(_webContext.CurrentUser == null)
                    pd.ShowFriendRequestButton = false;
            }
        }
    }
}
