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
using StructureMap;

namespace Fisharoo.FisharooWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private IUserSession _userSession;
        private IRedirector _redirector;
        private INavigation _navigation;

        public SiteMaster()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _navigation = ObjectFactory.GetInstance<INavigation>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _navigation.CheckAccessForCurrentNode();
            DoBrowserSniffing();

            repPrimaryNav.DataSource = _navigation.PrimaryNodes();
            repPrimaryNav.DataBind();

            repFooterNav.DataSource = _navigation.FooterNodes();
            repFooterNav.DataBind();

            if (_navigation.CurrentNode["PageTitle"] != null && !String.IsNullOrEmpty(_navigation.CurrentNode["PageTitle"]))
            {
                lblPageTitle.Text = _navigation.CurrentNode["PageTitle"].ToString();
                Page.Title = _navigation.CurrentNode["PageTitle"].ToString();
            }
            else
            {
                lblPageTitle.Text = _navigation.CurrentNode.Title;
                Page.Title = _navigation.CurrentNode.Title;
            }
        }

        //css issues worked out with browser sniffing
        protected string ContentMainLeft = "";
        protected string ContentHeight = "";
        private void DoBrowserSniffing()
        {

            switch (Request.Browser.Browser.ToLower())
            {
                case "mozilla":
                case "firefox":
                case "safari":
                    ContentMainLeft = "left:150px;";
                    break;
                case "ie":
                    ContentHeight = "height:423px;";
                    break;
            }
        }

        protected void repPrimaryNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink linkPrimaryNav = e.Item.FindControl("linkPrimaryNav") as HyperLink;
            SiteMapNode node = (SiteMapNode) e.Item.DataItem;

            linkPrimaryNav.Text = node.Title;
            linkPrimaryNav.NavigateUrl = node.Url;

            if (node == _navigation.CurrentNode || node == _navigation.CurrentNode.ParentNode)
            {
                linkPrimaryNav.CssClass = "PrimaryNavLinkActive";
            }
        }

        protected void repFooter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HyperLink linkFooterNav = e.Item.FindControl("linkFooterNav") as HyperLink;
            SiteMapNode node = (SiteMapNode) e.Item.DataItem;

            linkFooterNav.Text = node.Title;
            linkFooterNav.NavigateUrl = node.Url;
        }
    }
}
