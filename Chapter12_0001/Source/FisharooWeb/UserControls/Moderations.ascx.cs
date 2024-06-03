using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using Fisharoo.FisharooWeb.UserControls.Presenters;

namespace Fisharoo.FisharooWeb.UserControls
{
    public partial class Moderations : System.Web.UI.UserControl, IModerations
    {
        public int SystemObjectID { get; set; }
        public long SystemObjectRecordID { get; set; }
        public bool ShowFlagThis
        {
            set
            {
                pnlFlagThis.Visible = value;
            }
        }

        ModerationsPresenter _presenter = new ModerationsPresenter();


        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.Init(this, IsPostBack);
        }

        protected void ibFlagThis_Click(object sender, EventArgs e)
        {
            _presenter.SaveModeration(SystemObjectID, SystemObjectRecordID);
        }
    }
}