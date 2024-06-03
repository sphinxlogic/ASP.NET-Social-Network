using System.Web;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Redirector : IRedirector
    {
        public void GoToHomePage()
        {
            Redirect("~/Default.aspx");
        }

        public void GoToErrorPage()
        {
            Redirect("~/Error.aspx");
        }

        private void Redirect(string path)
        {
            HttpContext.Current.Response.Redirect(path);
        }
    }
}
