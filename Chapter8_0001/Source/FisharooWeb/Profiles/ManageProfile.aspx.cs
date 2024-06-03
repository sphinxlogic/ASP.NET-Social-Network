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
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using Fisharoo.FisharooWeb.Profiles.Presenter;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class ManageProfile : System.Web.UI.Page, IManageProfile
    {
        private ManageProfilePresenter _presenter;

        protected override void OnInit(EventArgs e)
        {
            _presenter = new ManageProfilePresenter();
            _presenter.Init(this,IsPostBack);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.LoadProfile(IsPostBack);
        }

        public void LoadProfileAttributeTypes(List<ProfileAttributeType> types)
        {
            foreach (ProfileAttributeType type in types)
            {
                Label lbl = new Label();
                lbl.ID = "lblAttribute" + type.ProfileAttributeTypeID.ToString();
                lbl.Text = type.AttributeType;

                Label lblAttributeTypeID = new Label();
                lblAttributeTypeID.ID = "lblAttributeTypeID" + type.ProfileAttributeTypeID.ToString();
                lblAttributeTypeID.Text = type.ProfileAttributeTypeID.ToString();
                lblAttributeTypeID.Visible = false;

                Label lblProfileAttributeID = new Label();
                lblProfileAttributeID.ID = "lblProfileAttributeID" + type.ProfileAttributeTypeID.ToString();
                lblProfileAttributeID.Visible = false;

                Label lblProfileAttributeTimestamp = new Label();
                lblProfileAttributeTimestamp.ID = "lblProfileAttributeTimestamp" +
                                                  type.ProfileAttributeTypeID.ToString();
                lblProfileAttributeTimestamp.Visible = false;
                
                TextBox tb = new TextBox();
                tb.ID = "txtProfileAttribute" + type.ProfileAttributeTypeID.ToString();
                tb.TextMode = TextBoxMode.MultiLine;
                tb.Columns = 20;
                tb.Rows = 3;

                CustomValidator cv = new CustomValidator();
                cv.ControlToValidate = "txtProfileAttribute" + type.ProfileAttributeTypeID.ToString();
                cv.ClientValidationFunction = "MaxLength2000";
                cv.ErrorMessage = "This field can only be 2000 characters long!";
                cv.Text = "*";
                cv.ForeColor = System.Drawing.Color.Red;


                phAttributes.Controls.Add(lblAttributeTypeID);
                phAttributes.Controls.Add(lblProfileAttributeID);
                phAttributes.Controls.Add(lblProfileAttributeTimestamp);
                phAttributes.Controls.Add(lbl);
                phAttributes.Controls.Add(new LiteralControl("<BR>"));
                phAttributes.Controls.Add(tb);
                phAttributes.Controls.Add(cv);
                phAttributes.Controls.Add(new LiteralControl("<BR>"));
            }
        }

        private List<ProfileAttribute> ExtractAttributes()
        {
            List<ProfileAttribute> attributes = new List<ProfileAttribute>();
            foreach (ProfileAttributeType type in _presenter.GetProfileAttributeTypes())
            {
                Label lblAttributeTypeID = phAttributes.FindControl("lblAttributeTypeID" + type.ProfileAttributeTypeID.ToString()) as Label;
                Label lblProfileAttributeID =
                    phAttributes.FindControl("lblProfileAttributeID" + type.ProfileAttributeTypeID.ToString()) as Label;
                Label lblProfileAttributeTimestamp =
                    phAttributes.FindControl("lblProfileAttributeTimestamp" + type.ProfileAttributeTypeID.ToString()) as
                    Label;
                TextBox txtProfileAttribute = phAttributes.FindControl("txtProfileAttribute" + type.ProfileAttributeTypeID.ToString()) as TextBox;
                ProfileAttribute pa = new ProfileAttribute();
                if (!string.IsNullOrEmpty(lblProfileAttributeID.Text))
                    pa.ProfileAttributeID = Convert.ToInt32(lblProfileAttributeID.Text);
                else
                    pa.ProfileAttributeID = 0;

                if (!string.IsNullOrEmpty(lblProfileAttributeID.Text) && !string.IsNullOrEmpty(lblProfileAttributeTimestamp.Text))
                    pa.TimeStamp = lblProfileAttributeTimestamp.Text.StringToTimestamp();

                pa.ProfileAttributeTypeID = Convert.ToInt32(lblAttributeTypeID.Text);
                pa.Response = txtProfileAttribute.Text;
                pa.CreateDate = DateTime.Now;
                attributes.Add(pa);
            }
            return attributes;
        }

        public void LoadLevelOfExperienceTypes(List<LevelOfExperienceType> types)
        {
            foreach (LevelOfExperienceType type in types)
            {
                ListItem li = new ListItem(type.LevelOfExperience,type.LevelOfExperienceTypeID.ToString());
                ddlLevelOfExperience.Items.Add(li);
            }
        }

        protected void wizProfile_NextButtonClick(object sender, EventArgs e)
        {
            lblErrorMessage.Text = "";
        }

        protected void wizProfile_FinishButtonClicked(object sender, EventArgs e)
        {
            Profile profile = _presenter.GetProfile();
            if(profile == null)
                profile = new Profile();

            Account account = _presenter.GetAccount();

            if (!string.IsNullOrEmpty(lblProfileID.Text))
                profile.ProfileID = Convert.ToInt32(lblProfileID.Text);

            profile.AccountID = account.AccountID;
            profile.IMAOL = txtIMAOL.Text;
            profile.IMGIM = txtIMGIM.Text;
            profile.IMICQ = txtIMICQ.Text;
            profile.IMMSN = txtIMMSN.Text;
            profile.IMSkype = txtIMSkype.Text;
            profile.IMYIM = txtIMYIM.Text;
            profile.LevelOfExperienceTypeID = Convert.ToInt32(ddlLevelOfExperience.SelectedValue);
            profile.Signature = txtSignature.Text;

            profile.Attributes = ExtractAttributes();

            if (!string.IsNullOrEmpty(txtNumberOfFishOwned.Text))
                profile.NumberOfFishOwned = Convert.ToInt32(txtNumberOfFishOwned.Text);

            if (!string.IsNullOrEmpty(txtNumberOfTanksOwned.Text))
                profile.NumberOfTanksOwned = Convert.ToInt32(txtNumberOfTanksOwned.Text);

            if (!string.IsNullOrEmpty(txtYearOfFirstTank.Text))
                profile.YearOfFirstTank = Convert.ToInt32(txtYearOfFirstTank.Text);

             _presenter.SaveProfile(profile);
        }

        public void ShowMessage(string Message)
        {
            lblErrorMessage.Text = Message;
        }

        public void LoadProfile(Profile profile)
        {
            if (profile != null)
            {
                lblProfileID.Text = profile.ProfileID.ToString();
                txtIMAOL.Text = profile.IMAOL;
                txtIMGIM.Text = profile.IMGIM;
                txtIMICQ.Text = profile.IMICQ;
                txtIMMSN.Text = profile.IMMSN;
                txtIMSkype.Text = profile.IMSkype;
                txtIMYIM.Text = profile.IMYIM;
                txtNumberOfFishOwned.Text = profile.NumberOfFishOwned.ToString();
                txtNumberOfTanksOwned.Text = profile.NumberOfTanksOwned.ToString();
                txtSignature.Text = profile.Signature;
                txtYearOfFirstTank.Text = profile.YearOfFirstTank.ToString();

                foreach (ListItem item in ddlLevelOfExperience.Items)
                {
                    if(item.Value == profile.LevelOfExperienceTypeID.ToString())
                        item.Selected = true;
                }

                if (profile.Attributes.Count > 0)
                {
                    foreach (ProfileAttribute attribute in profile.Attributes)
                    {
                        Label lblProfileAttributeID =
                            phAttributes.FindControl("lblProfileAttributeID" +
                                                     attribute.ProfileAttributeTypeID.ToString()) as Label;
                        Label lblProfileAttributeTimestamp =
                            phAttributes.FindControl("lblProfileAttributeTimestamp" +
                                                     attribute.ProfileAttributeTypeID.ToString()) as Label;

                        if (lblProfileAttributeID != null)
                        {
                            lblProfileAttributeID.Text = attribute.ProfileAttributeID.ToString();
                        }

                        if(lblProfileAttributeTimestamp != null)
                        {
                            lblProfileAttributeTimestamp.Text = attribute.TimeStamp.TimestampToString();
                        }

                        TextBox txtProfileAttribute =
                            phAttributes.FindControl("txtProfileAttribute" + attribute.ProfileAttributeTypeID.ToString()) as
                            TextBox;
                        if (txtProfileAttribute != null)
                        {
                            txtProfileAttribute.Text = attribute.Response;
                        }
                    }
                }
            }
        }
    }
}
