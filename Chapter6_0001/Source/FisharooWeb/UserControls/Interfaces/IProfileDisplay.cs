using System;
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
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.UserControls.Interfaces
{
    public interface IProfileDisplay
    {
        void LoadDisplay(Account account);
        bool ShowFriendRequestButton { set; }
        bool ShowDeleteButton { set; }
    }
}
