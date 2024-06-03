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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Friends.Interface;
using Fisharoo.FisharooWeb.Friends.Presenter;
using Fisharoo.FisharooWeb.UserControls;

namespace Fisharoo.FisharooWeb.Friends
{
    public partial class Default : System.Web.UI.Page, IDefault 
    {
        private DefaultPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultPresenter();
            _presenter.Init(this);
        }

        protected void repFriends_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ProfileDisplay pdProfileDisplay = e.Item.FindControl("pdProfileDisplay") as ProfileDisplay;
                pdProfileDisplay.LoadDisplay(((Account)e.Item.DataItem));
            }
        }

        public void LoadDisplay(List<Account> Accounts)
        {
            repFriends.DataSource = Accounts;
            repFriends.DataBind();
        }
    }
}
