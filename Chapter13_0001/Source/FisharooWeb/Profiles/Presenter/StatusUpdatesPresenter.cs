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
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles.Presenter
{
    public class StatusUpdatesPresenter
    {
        private IStatusUpdates _view;
        private IStatusUpdateRepository _statusUpdateRepository;
        private Int32 _accountIDToShow;

        public StatusUpdatesPresenter()
        {
            _statusUpdateRepository = ObjectFactory.GetInstance<IStatusUpdateRepository>();
        }

        public void Init(IStatusUpdates view, Int32 AccountIDToShow)
        {
            _view = view;
            _accountIDToShow = AccountIDToShow;
            ShowStatusUpdates();
        }

        private void ShowStatusUpdates()
        {
            _view.ShowUpdates(_statusUpdateRepository.GetStatusUpdatesByAccountID(_accountIDToShow));
        }
    }
}
