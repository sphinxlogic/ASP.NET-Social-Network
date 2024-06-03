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
            foreach (SiteMapNode node in SiteMap.RootNode.ChildNodes)
            {
                    nodes.Add(node);
            }
            return nodes;
        }

        public List<SiteMapNode> PrimaryNodes()
        {
            List<SiteMapNode> primaryNodes = new List<SiteMapNode>();
            foreach (SiteMapNode node in AllNodes())
            {
                if (node["topnav"] != null && CheckAccessForNode(node))
                    primaryNodes.Add(node);
            }
            return primaryNodes;
        }

        public List<SiteMapNode> FooterNodes()
        {
            List<SiteMapNode> footerNodes = new List<SiteMapNode>();
            foreach (SiteMapNode node in AllNodes())
            {
                if (node["footernav"] != null && CheckAccessForNode(node))
                    footerNodes.Add(node);
            }
            return footerNodes;
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
