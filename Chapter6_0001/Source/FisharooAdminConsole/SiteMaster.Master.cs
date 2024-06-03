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
using StructureMap;

namespace Fisharoo.FisharooAdminConsole
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private IUserSession _userSession;
        private IRedirector _redirector;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();

            CheckLogin();

            repPrimaryNav.DataSource = loadPrimaryNodes();
            repPrimaryNav.DataBind();
            repSecondaryNav.DataSource = loadSecondaryNodes();
            repSecondaryNav.DataBind();

            if (SiteMap.CurrentNode["PageTitle"] != null && !String.IsNullOrEmpty(SiteMap.CurrentNode["PageTitle"]))
            {
                lblPageTitle.Text = SiteMap.CurrentNode["PageTitle"].ToString();
                Page.Title = SiteMap.CurrentNode["PageTitle"].ToString();
            }
            else
            {
                lblPageTitle.Text = SiteMap.CurrentNode.Title;
                Page.Title = SiteMap.CurrentNode.Title;
            }
        }

        protected void CheckLogin()
        {
            if (!_userSession.LoggedIn)
                _redirector.GoToAccountLoginPage();
        }

        private List<SiteMapNode> loadPrimaryNodes()
        {
            List<SiteMapNode> primaryNodes = new List<SiteMapNode>();
            primaryNodes.Add(SiteMap.RootNode);
            foreach (SiteMapNode node in SiteMap.RootNode.GetAllNodes())
            {
                if(node["topnav"] != null)
                    primaryNodes.Add(node);
            }
            return primaryNodes;
        }

        private List<SiteMapNode> loadSecondaryNodes()
        {
            List<SiteMapNode> secondaryNodes = new List<SiteMapNode>();
            
            foreach (SiteMapNode node in SiteMap.RootNode.GetAllNodes())
            {
                if((node.ParentNode == SiteMap.CurrentNode || SiteMap.CurrentNode.ParentNode == node.ParentNode) 
                    && node["topnav"] == null)
                {
                    secondaryNodes.Add(node);
                }
            }
            return secondaryNodes;
        }

        protected void repPrimaryNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkPrimaryNav = e.Item.FindControl("linkPrimaryNav") as HyperLink;
                SiteMapNode node = (SiteMapNode)e.Item.DataItem;

                linkPrimaryNav.Text = node.Title;
                linkPrimaryNav.NavigateUrl = node.Url;

                if (node == SiteMap.CurrentNode || node == SiteMap.CurrentNode.ParentNode)
                {
                    linkPrimaryNav.CssClass = "PrimaryNavLinkActive"; ;
                }
            }
        }

        protected void repSecondaryNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkSecondaryNav = e.Item.FindControl("linkSecondaryNav") as HyperLink;
                SiteMapNode node = (SiteMapNode)e.Item.DataItem;

                linkSecondaryNav.Text = node.Title;
                linkSecondaryNav.NavigateUrl = node.Url;

                if (node == SiteMap.CurrentNode || node == SiteMap.CurrentNode.ParentNode)
                {
                    linkSecondaryNav.CssClass = "PrimaryNavLinkActive"; ;
                }
            }
        }
    }
}
