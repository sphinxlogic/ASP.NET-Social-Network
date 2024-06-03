using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Fisharoo.FisharooWeb.Friends.Interface
{
    public interface IOutlookCsvImporter
    {
        void ShowParsedEmail(List<string> Emails);
        void ShowInvitationResult(string Message);
    }
}
