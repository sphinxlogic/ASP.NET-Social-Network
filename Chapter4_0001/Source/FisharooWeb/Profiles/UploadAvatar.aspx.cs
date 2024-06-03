using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using Fisharoo.FisharooWeb.Profiles.Presenter;
using StructureMap;
using Image=System.Web.UI.WebControls.Image;

namespace Fisharoo.FisharooWeb.Profiles
{
    public partial class UploadAvatar: System.Web.UI.Page, IUploadAvatar
    {
        private IProfileRepository _profileRepository;
        private IUserSession _userSession;
        private IRedirector _redirector;
        private Profile profile;

        private UploadAvatarPresenter _presenter;

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new UploadAvatarPresenter();
            _presenter.Init(this);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if(fuAvatarUpload.PostedFile != null || !string.IsNullOrEmpty(fuAvatarUpload.PostedFile.FileName))
            {
                _presenter.UploadFile(fuAvatarUpload.PostedFile);
            }
        }

        public void ShowMessage(string Message)
        {
            lblMessage.Text = Message;    
        }
        public void ShowApprovePanel()
        {
            pnlCrop.Visible = false;
            pnlUpload.Visible = false;
            pnlApprove.Visible = true;
        }

        public void ShowUploadPanel()
        {
            pnlCrop.Visible = false;
            pnlUpload.Visible = true;
            pnlApprove.Visible = false;
        }

        public void ShowCropPanel()
        {
            pnlCrop.Visible = true;
            pnlUpload.Visible = false;
            pnlApprove.Visible = false;
        }
        protected void btnCrop_Click(object sender, EventArgs e)
        {
            _presenter.CropFile(Convert.ToInt32(hidX1.Value),
                Convert.ToInt32(hidY1.Value),
                Convert.ToInt32(hidWidth.Value),
                Convert.ToInt32(hidHeight.Value));    
        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            _presenter.Complete();
        }

        protected void btnStartNew_Click(object sender, EventArgs e)
        {
            _presenter.StartNewAvatar();
        }

        protected void btnUseGravatar_Click(object sender, EventArgs e)
        {
            if (cbUseGravatar.Checked)
                _presenter.UseGravatar();
            else
                lblMessage.Text = "Did you mean to check the box?";   
        }
    }
}
