using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using Fisharoo.FisharooWeb.Profiles.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class StatusUpdates : System.Web.UI.Page, IStatusUpdates 
    {
        private StatusUpdatesPresenter _presenter;
        private IWebContext _webContext;
        private IUserSession _userSession;

        protected void Page_Load(object sender, EventArgs e)
        { 
            _presenter = new StatusUpdatesPresenter();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();

            if(_webContext.AccountID > 0)
                _presenter.Init(this, _webContext.AccountID);
            else if(_userSession.CurrentUser != null)
                _presenter.Init(this, _userSession.CurrentUser.AccountID);
        }

        public void ShowUpdates(List<StatusUpdate> StatusUpdates)
        {
            repStatusUpdates.DataSource = StatusUpdates;
            repStatusUpdates.DataBind();
        }
    }
}
