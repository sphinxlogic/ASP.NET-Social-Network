using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using Fisharoo.FisharooWeb.UserControls.Presenters;
using AjaxControlToolkit;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls
{
    public partial class Ratings : System.Web.UI.UserControl, IRatings 
    {
        public int SystemObjectID { get; set; }
        public long SystemObjectRecordID { get; set; }
        private RatingsPresenter _presenter;
        private IWebContext _webContext;
        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new RatingsPresenter();
            _presenter.Init(this, IsPostBack);
        }

        public void SetCurrentRating(int CurrentRating)
        {
            Rating1.CurrentRating = CurrentRating;
        }

        public void LoadOptions(List<SystemObjectRatingOption> Options)
        {
            repRatingOptions.DataSource = Options;
            repRatingOptions.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _presenter.btnSave_Click(sender, e, SystemObjectID, SystemObjectRecordID);

            pnlModalPopup.Visible = false;
            lbRateThis.Visible = false;
            Rating1.Visible = false;

            lblThankYou.Visible = true;
        }

        protected void rating_Changed(object sender, RatingEventArgs args)
        {
            _presenter.rating_Changed(sender, args);
        }

        protected void lbRateThis_Click(object sender, EventArgs e)
        {
            pnlModalPopup.Visible = true;
        }

        public void CanSetRating(bool Visible)
        {
            lbRateThis.Visible = Visible;
            pnlModalPopup.Visible = Visible;
        }
    }
}