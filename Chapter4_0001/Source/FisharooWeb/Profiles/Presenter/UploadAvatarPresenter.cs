using System;
using System.Data;
using System.Configuration;
using System.Drawing;
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
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.Profiles.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Profiles.Presenter
{
    public class UploadAvatarPresenter
    {
        private IProfileRepository _profileRepository;
        private IUserSession _userSession;
        private IRedirector _redirector;
        private Profile profile;
        private IUploadAvatar _view;
        private IAlertService _alertService;

        public UploadAvatarPresenter()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _profileRepository = ObjectFactory.GetInstance<IProfileRepository>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
            _alertService = ObjectFactory.GetInstance<IAlertService>();
        }

        public void Init(IUploadAvatar View)
        {
            _view = View;

            profile = _profileRepository.GetProfileByAccountID(_userSession.CurrentUser.AccountID);
            if (profile == null || profile.ProfileID < 1)
                _redirector.GoToAccountLoginPage();
        }

        public void UseGravatar()
        {
            profile.UseGravatar = 1;
            _profileRepository.SaveProfile(profile);
            _alertService.AddNewAvatarAlert();
            _redirector.GoToProfilesProfile();
        }

        public void StartNewAvatar()
        {
             _view.ShowUploadPanel();   
        }

        public void Complete()
        {
            _alertService.AddNewAvatarAlert();
            _redirector.GoToProfilesProfile(); 
        }

        public void CropFile(Int32 X, Int32 Y, Int32 Width, Int32 Height)
        {
            //get byte array from profile
            byte[] imageBytes = profile.Avatar.ToArray();
            //stuff this byte array into a memory stream
            using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                //write the memory stream out for use
                ms.Write(imageBytes, 0, imageBytes.Length);

                //stuff the memory stream into an image to work with
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms, true);

                //create the destination (cropped) bitmap
                Bitmap bmpCropped = new Bitmap(200, 200);

                //create the graphics object to draw with
                Graphics g = Graphics.FromImage(bmpCropped);

                Rectangle rectDestination = new Rectangle(0, 0, bmpCropped.Width, bmpCropped.Height);
                Rectangle rectCropArea = new Rectangle(X,Y,Width,Height);

                //draw the rectCropArea of the original image to the rectDestination of bmpCropped
                g.DrawImage(img, rectDestination, rectCropArea, GraphicsUnit.Pixel);

                //release system resources
                g.Dispose();

                MemoryStream stream = new MemoryStream();
                bmpCropped.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                Byte[] bytes = stream.ToArray();

                profile.Avatar = bytes;
                _profileRepository.SaveProfile(profile);
            }
            _view.ShowApprovePanel();
        }

        public void UploadFile(HttpPostedFile File)
        {
            string extension = Path.GetExtension(File.FileName).ToLower();
            string mimetype;
            byte[] uploadedImage = new byte[File.InputStream.Length];
            switch (extension)
            {
                case ".png":
                case ".jpg":
                case ".gif":
                    mimetype = File.ContentType;
                    break;

                default:
                    _view.ShowMessage("We only accept .png, .jpg, and .gif!");
                    return;
                    break;
            }

            if (File.ContentLength / 1000 < 1000)
            {
                File.InputStream.Read(uploadedImage, 0, uploadedImage.Length);
                profile.Avatar = uploadedImage;
                profile.AvatarMimeType = mimetype;
                profile.UseGravatar = 0;
                _profileRepository.SaveProfile(profile);
                _view.ShowCropPanel();

            }
            else
            {
                _view.ShowMessage("The file you uploaded is larger than the 1mb limit.  Please reduce the size of your file and try again.");
            }
        }
    }
}
