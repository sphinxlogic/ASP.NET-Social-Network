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
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooWeb.Profiles.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles.Presenter
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IAlertService _alertService;
        private IUserSession _userSession;

        public DefaultPresenter()
        {
            _alertService = ObjectFactory.GetInstance<IAlertService>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
        }
        public void Init(IDefault view)
        {
            _view = view;
            ShowDisplay();
        }

        private void ShowDisplay()
        {
            _view.ShowAlerts(_alertService.GetAlertsByAccountID(_userSession.CurrentUser.AccountID));
        }
    }
}
