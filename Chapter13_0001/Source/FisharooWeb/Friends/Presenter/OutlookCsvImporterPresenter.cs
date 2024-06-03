using System;
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
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooWeb.Friends.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Friends.Presenter
{
    public class OutlookCsvImporterPresenter
    {
        private IOutlookCsvImporter _view;
        private IEmail _email;
        private IUserSession _userSession;
        public OutlookCsvImporterPresenter()
        {
            _email = ObjectFactory.GetInstance<IEmail>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }

        public void Init(IOutlookCsvImporter view)
        {
            _view = view;
        }

        public void ParseEmails(HttpPostedFile file)
        {
            using (Stream s = file.InputStream)
            {
                StreamReader sr = new StreamReader(s);
                string contacts = sr.ReadToEnd();

                _view.ShowParsedEmail(_email.ParseEmailsFromText(contacts));
            }
        }

        public void InviteContacts(string ToEmailArray)
        {
            string result = _email.SendInvitations(_userSession.CurrentUser, ToEmailArray, "");
            _view.ShowInvitationResult(result);
        }
    }
}
