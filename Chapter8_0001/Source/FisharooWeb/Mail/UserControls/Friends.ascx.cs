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
using Fisharoo.FisharooWeb.Mail.UserControls.Interface;
using Fisharoo.FisharooWeb.Mail.UserControls.Presenter;

namespace Fisharoo.FisharooWeb.Mail.UserControls
{
    public partial class Friends : System.Web.UI.UserControl, IFriends
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FriendsPresenter _presenter = new FriendsPresenter();
            _presenter.Init(this);
        }

        public void LoadFriends(List<Account> Friends)
        {
            repFriends.DataSource = Friends;
            repFriends.DataBind();
        }

        public void repFriends_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkFriend = e.Item.FindControl("linkFriend") as HyperLink;
                linkFriend.Attributes.Add("OnClick", "javascript:document.forms[0].ctl00_Content_txtTo.value += '" + ((Account)e.Item.DataItem).Username + ";';");
            }
        }
    }
}