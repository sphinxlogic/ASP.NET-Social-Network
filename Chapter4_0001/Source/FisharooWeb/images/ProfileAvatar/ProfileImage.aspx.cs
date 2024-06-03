using System;
using System.Collections;
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
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooWeb.images.ProfileAvatar
{
    public partial class ProfileImage : System.Web.UI.Page
    {
        private IProfileRepository _profileRepository;
        private IUserSession _userSession;
        private IAccountRepository _accountRepository;
        private IWebContext _webContext;

        private Int32 accountID;
        private Account account;
        private Profile profile;
        private string gravatarURL;
        private string defaultAvatar;
        private bool showGravatar;

        protected void Page_Load(object sender, EventArgs e)
        {
            _profileRepository = ObjectFactory.GetInstance<IProfileRepository>();
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();

            //load an image by passed in accountid
            if(_webContext.AccountID > 0)
            {
                accountID = _webContext.AccountID;
                profile = _profileRepository.GetProfileByAccountID(accountID);
                account = _accountRepository.GetAccountByID(accountID);
            }
            //get an image for the current user
            else
            {
                if(_userSession.LoggedIn && _userSession.CurrentUser != null)
                {
                    account = _userSession.CurrentUser;
                    profile = _profileRepository.GetProfileByAccountID(account.AccountID);
                }
            }
            
            //show the appropriate image
            if(_webContext.ShowGravatar || profile.UseGravatar == 1)
            {
                Response.Redirect(GetGravatarURL());
            }
            else if (profile != null && profile.Avatar != null)
            {
                Response.Clear();
                Response.ContentType = profile.AvatarMimeType;
                Response.BinaryWrite(profile.Avatar.ToArray());
            }
            else
            {
                Response.Redirect("~/images/ProfileAvatar/Male.jpg");
            }
        }

public string GetGravatarURL()
{
    defaultAvatar = Server.UrlPathEncode(_webContext.RootUrl + "/images/ProfileAvatar/Male.jpg");

    gravatarURL = "http://www.gravatar.com/avatar.php?";
    gravatarURL += "gravatar_id=" + account.Email.ToMD5Hash();
    gravatarURL += "&rating=r";
    gravatarURL += "&size=80";
    gravatarURL += "&default=" + defaultAvatar;
    return gravatarURL;
}
    }
}
