using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core.DataAccess;
using StructureMap;

namespace Fisharoo.FisharooWeb.Handlers
{
    //CHAPTER 4
    public class UrlRewrite : IHttpModule
    {
        private IAccountRepository _accountRepository;
        public UrlRewrite()
        {
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
        }

        public void Init(HttpApplication application)
        {
            //let's register our event handler
            application.PostResolveRequestCache +=
                (new EventHandler(this.Application_OnAfterProcess));
        }

        public void Dispose()
        {
            
        }

        private void Application_OnAfterProcess(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;

            //the file being requested is not found
            //so let's look, maybe it's our "fake" file
            if (!File.Exists(application.Request.PhysicalPath))
            {
                string username = application.Request.Path.Replace("/", "");
                FisharooCore.Core.Domain.Account account = _accountRepository.GetAccountByUsername(username);
                if(account != null)
                {
                    string UserURL = "default.aspx?pid=" + account.AccountID.ToString();
                    context.RewritePath(UserURL);
                }
                else
                {
                    context.Response.Redirect("~/PageNotFound.aspx");   
                }
            }
        }
    }
}
