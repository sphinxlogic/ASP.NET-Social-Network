using System;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisahroo.FisharooWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Log.EnsureInitialized();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Log.Error(sender,"Error caught by the Global.asax: " + e.ToString());

            IRedirector redir = ObjectFactory.GetInstance<IRedirector>();
            redir.GoToHomePage();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}