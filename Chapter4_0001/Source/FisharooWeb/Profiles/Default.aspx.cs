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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using Fisharoo.FisharooWeb.Profiles.Presenter;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class Default : System.Web.UI.Page, IDefault
    {
        private DefaultPresenter _presenter;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultPresenter();
            _presenter.Init(this);
        }

        public void ShowAlerts(List<Alert> alerts)
        {
            repFilter.DataSource = alerts;
            repFilter.DataBind();
        }
    }
}
