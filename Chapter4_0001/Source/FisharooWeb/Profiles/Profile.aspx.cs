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
using Fisharoo.FisharooCore.Core.Impl;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class ViewProfile : System.Web.UI.Page, IProfile 
    {
        private ProfilePresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ProfilePresenter();
            _presenter.Init(this);
        }

        public void SetAvatar(Int32 AccountID)
        {
            if(!imgAvatar.ImageUrl.Contains("AccountID="))
                imgAvatar.ImageUrl += "?AccountID=" + AccountID.ToString();
        }

        public void pnlPrivacyTankInfoVisible(bool value)
        {
            pnlPrivacyTankInfo.Visible = value; 
        }
        public void pnlPrivacyAccountInfoVisible(bool value)
        {
            pnlPrivacyAccountInfo.Visible = value;
        }
        public void pnlPrivacyIMVisible(bool value)
        {
            pnlPrivacyIM.Visible = value;
        }

        public void DisplayInfo(Account account)
        {
            lblFirstName.Text = account.FirstName;
            lblLastName.Text = account.LastName;
            litLastUpdateDate.Text = account.LastUpdateDate.ToString();
            litZip.Text = account.Zip;
            litBirthDate.Text = account.BirthDate.ToString();
            litEmail.Text = account.Email;

            if (account.Profile != null)
            {
                litIMAOL.Text = account.Profile.IMAOL;
                litIMGIM.Text = account.Profile.IMGIM;
                litIMICQ.Text = account.Profile.IMICQ;
                litIMMSN.Text = account.Profile.IMMSN;
                litIMSkype.Text = account.Profile.IMSkype;
                litIMYIM.Text = account.Profile.IMYIM;
                litNumberOfTanksOwned.Text = account.Profile.NumberOfFishOwned.ToString();
                litNumberOfTanksOwned.Text = account.Profile.NumberOfTanksOwned.ToString();
                litYearOfFirstTank.Text = account.Profile.YearOfFirstTank.ToString();
                litLevelOfExperience.Text = "(" + account.Profile.LevelOfExperienceType.LevelOfExperience + ")";
                if(account.Profile.Attributes.Count > 0)
                {
                    foreach (ProfileAttribute attribute in account.Profile.Attributes)
                    {
                        if (_presenter.IsAttributeVisible(attribute.ProfileAttributeType.PrivacyFlagTypeID))
                        {
                            phAttributes.Controls.Add(new LiteralControl("<div class=\"divContainerTitle\">"));
                            phAttributes.Controls.Add(new LiteralControl(attribute.ProfileAttributeType.AttributeType));
                            phAttributes.Controls.Add(new LiteralControl("</div>"));
                            phAttributes.Controls.Add(new LiteralControl("<div class=\"divContainerRow\">"));
                            phAttributes.Controls.Add(new LiteralControl(attribute.Response));
                            phAttributes.Controls.Add(new LiteralControl("</div>"));
                        }
                    }
                }
            }
        }
    }
}
