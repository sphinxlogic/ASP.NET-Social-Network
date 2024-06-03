using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Navigation : INavigation
    {
        private IUserSession _userSession;
        private IRedirector _redirector;
        private Account _account;

        public Navigation()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _account = _userSession.CurrentUser;
        }
        
        public List<SiteMapNode> AllNodes()
        {
            List<SiteMapNode> nodes = new List<SiteMapNode>();
            nodes.Add(SiteMap.RootNode);
            foreach (SiteMapNode node in SiteMap.RootNode.GetAllNodes())
            {
                if(CheckAccessForNode(node) && NodeIsVisible(node))
                    nodes.Add(node);
            }
            return nodes;
        }

        public List<SiteMapNode> PrimaryNodes()
        {
            List<SiteMapNode> primaryNodes = new List<SiteMapNode>();
            foreach (SiteMapNode node in AllNodes())
            {
                if (node["topnav"] != null && CheckAccessForNode(node) && NodeIsVisible(node))
                    primaryNodes.Add(node);
            }
            return primaryNodes;
        }

        public List<SiteMapNode> SecondaryNodes()
        {
            List<SiteMapNode> result = new List<SiteMapNode>();
            SiteMapNode parentNode;

            //is the current node the root node? aka home page
            if(CurrentNode == SiteMap.RootNode)
            {
                return GetRootSecondaryNodes();
            }
            //is the current node a topnav node?
            else if (CurrentNode["topnav"] != null)
            {
                parentNode = CurrentNode;
                foreach (SiteMapNode node in parentNode.ChildNodes)
                {
                    if (CheckAccessForNode(node) && NodeIsVisible(node))
                        result.Add(node);
                }
                
            }
            //not a topnav node...find the topnav node
            else
            {
                parentNode = CurrentNode.ParentNode;
                
                if (parentNode == SiteMap.RootNode)
                    return GetRootSecondaryNodes();

                for(int i = 0;i==5;i++)
                {
                    if (parentNode["topnav"] != null)
                        break;
                    else
                        parentNode = parentNode.ParentNode;
                }
                foreach (SiteMapNode node in parentNode.ChildNodes)
                {
                    if(CheckAccessForNode(node) && NodeIsVisible(node))
                        result.Add(node);
                }
            }
            return result;
        }

        private List<SiteMapNode> GetRootSecondaryNodes()
        {
            List<SiteMapNode> secondaryNodes = new List<SiteMapNode>();
            foreach (SiteMapNode node in AllNodes())
            {
                if (node["rootSecondaryNav"] != null && NodeIsVisible(node) && CheckAccessForNode(node))
                    secondaryNodes.Add(node);
            }
            return secondaryNodes;
        }

        public List<SiteMapNode> FooterNodes()
        {
            List<SiteMapNode> footerNodes = new List<SiteMapNode>();
            foreach (SiteMapNode node in AllNodes())
            {
                if (node["footernav"] != null && NodeIsVisible(node) && CheckAccessForNode(node))
                    footerNodes.Add(node);
            }
            return footerNodes;
        }

        private bool NodeIsVisible(SiteMapNode node)
        {
            if(node["visible"] != null && node["visible"] == "1")
                return true;
            return false;
        }

        private bool CheckAccessForNode(SiteMapNode node)
        {
            if (!node.Roles.Contains("PUBLIC"))
            {
                if (_account != null && _account.Permissions != null && _account.Permissions.Count > 0)
                {
                    foreach (string role in node.Roles)
                    {
                        if (!_account.HasPermission(role))
                            return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }

        public void CheckAccessForCurrentNode()
        {
            bool result = CheckAccessForNode(CurrentNode);
            if(result)
                return;
            else
                _redirector.GoToAccountAccessDenied();
        }

        public SiteMapNode RootNode
        {
            get { return SiteMap.RootNode; }
        }

        public SiteMapNode CurrentNode
        {
            get
            {
                return SiteMap.CurrentNode;
            }
        }
    }
}
