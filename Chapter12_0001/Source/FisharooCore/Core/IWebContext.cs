using System;
using System.Collections.Generic;
using System.Web;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IWebContext
    {
        int TagID { get; }
        void ClearSelectedRatings();
        Dictionary<int, int> SelectedRatings { get; set; }
        HttpFileCollection Files { get; }
        bool NewGroup { get; }
        Int32 GroupID { get; }
        string CategoryPageName { get; }
        string ForumPageName { get; }
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
        Int32 PageNumber { get;  }
        Int64 AlbumID { get; }
        Int32 FileTypeID { get; }
        Int64 BlogID { get; }
        Int32 ForumID { get;  }
        Int64 PostID { get; }
        bool IsThread { get; }
    }
}