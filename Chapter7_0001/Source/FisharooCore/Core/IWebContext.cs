using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IWebContext
    {
        string FilePath { get; }
        string FilePathToPhotos { get; }
        string FilePathToVideos { get; }
        string FilePathToAudios { get; }
        void ClearSession();
        bool ContainsInSession(string key);
        void RemoveFromSession(string key);

        string FriendshipRequest{ get; }
        string SearchText{ get; }
        Int32 AccoundIdToInvite{ get; }

        Int32 AccountID { get; }
        bool ShowGravatar { get; }
        string RootUrl { get; }
        bool LoggedIn { get; set; }
        string Username { get; set;  }
        Account CurrentUser { get; set; }
        string CaptchaImageText { get; set; }
        string UsernameToVerify { get; }
        Int32 MessageID { get; }
        Int32 FolderID { get; }
        Int32 Page { get;  }
        Int64 AlbumID { get; }
        Int32 FileTypeID { get; }
    }
}