using System;
using System.Collections.Generic;
using System.Web;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class WebContext : IWebContext
    {
        public void ClearSelectedRatings()
        {
            SetInSession("SelectedRatings", null);    
        }

        public int TagID
        {
            get
            {
                int result = 0;
                if (GetQueryStringValue("TagID") != null)
                {
                    result = Convert.ToInt32((GetQueryStringValue("TagID")));
                }
                return result;
            }
        }

        public Dictionary<int, int> SelectedRatings
        {
            get
            {
                Dictionary<int, int> result = new Dictionary<int, int>();
                if(GetFromSession("SelectedRatings") != null)
                {
                    result = GetFromSession("SelectedRatings") as Dictionary<int, int>;
                }
                return result;
            }
            set
            {
                //make sure that we add to the existing rating store rather
                //than creating a new one
                Dictionary<int, int> result = new Dictionary<int, int>();
                if (GetFromSession("SelectedRatings") != null)
                {
                    result = GetFromSession("SelectedRatings") as Dictionary<int, int>;
                    foreach (KeyValuePair<int, int> pair in value)
                    {
                        if (!result.ContainsKey(pair.Key))
                            result.Add(pair.Key, pair.Value);
                    }
                    SetInSession("SelectedRatings", result);
                }
                else
                    SetInSession("SelectedRatings", value);

                
            }
        }

        public HttpFileCollection Files
        {
            get
            {
                if (HttpContext.Current.Request.Files != null)
                    return HttpContext.Current.Request.Files;
                else
                    return null;
            }
        }

        public bool NewGroup
        {
            get
            {
                bool result = false;
                if(!string.IsNullOrEmpty(GetQueryStringValue("NewGroup")))
                {
                    if(GetQueryStringValue("NewGroup") == "1")
                        result = true;
                }
                return result;
            }
        }
        public Int32 GroupID
        {
            get
            {
                Int32 result = 0;
                if(!string.IsNullOrEmpty(GetQueryStringValue("GroupID")))
                {
                    result = Convert.ToInt32(GetQueryStringValue("GroupID"));
                }
                return result;
            }
        }

        public bool IsThread
        {
            get
            {
                bool result = false;
                if(!string.IsNullOrEmpty(GetQueryStringValue("IsThread")))
                {
                    if(GetQueryStringValue("IsThread") == "1")
                        result = true;
                }
                return result;
            }
        }

        public Int64 PostID
        {
            get
            {
                Int64 result = 0;
                if(!string.IsNullOrEmpty(GetQueryStringValue("PostID")))
                {
                    result = Convert.ToInt64(GetQueryStringValue("PostID"));
                }
                return result;
            }
        }
        //CHAPTER 9
        public string CategoryPageName
        {
            get
            {
                string result = "";
                if (!string.IsNullOrEmpty(GetQueryStringValue("CategoryPageName")))
                {
                    result = GetQueryStringValue("CategoryPageName");
                }
                return result;
            }
        }
        //CHAPTER 9
        public string ForumPageName
        {
            get
            {
                string result = "";
                if (!string.IsNullOrEmpty(GetQueryStringValue("ForumPageName")))
                {
                    result = GetQueryStringValue("ForumPageName");
                }
                return result;
            }
        }
        //CHAPTER 9
        public Int32 ForumID
        {
            get
            {
                Int32 result = 0;
                if(!string.IsNullOrEmpty(GetQueryStringValue("ForumID")))
                {
                    result = Convert.ToInt32(GetQueryStringValue("ForumID"));
                }
                return result;
            }
        }
        public Int64 BlogID
        {
            get
            {
                Int64 result = 0;
                if(!string.IsNullOrEmpty(GetQueryStringValue("BlogID")))
                {
                    result = Convert.ToInt64(GetQueryStringValue("BlogID"));
                }
                return result;
            }
        }
        public string FilePath
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"].ToString();
            }
        }
        public string FilePathToPhotos
        {
            get
            {
                 return this.FilePath + "Photos\\";
            }
        }
        public string FilePathToVideos
        {
            get
            {
                return this.FilePath + "Videos\\";
            }
        }
        public string FilePathToAudios
        {
            get
            {
                return this.FilePath + "Audios\\";
            }
        }
        public Int32 FileTypeID
        {
            get
            {
                Int32 result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("FileTypeID")))
                    result = Convert.ToInt32(GetQueryStringValue("FileTypeID"));
                else
                    result = 0;

                return result;
            }
        }
        public Int64 AlbumID
        {
            get
            {
                Int64 result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("AlbumID")))
                    result = Convert.ToInt64(GetQueryStringValue("AlbumID"));
                else
                    result = 0;

                return result;
            }
        }
        public Int32 PageNumber
        {
            get
            {
                Int32 result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("PageNumber")))
                    result = Convert.ToInt32(GetQueryStringValue("PageNumber"));
                else
                    result = 1;
                return result;
            }
        }
        public Int32 FolderID
        {
            get
            {
                Int32 result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("Folder")))
                    result = Convert.ToInt32(GetQueryStringValue("Folder"));
                else
                    result = 1;
                return result;
            }
        }
        public Int32 MessageID
        {
            get
            {
                Int32 result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("MessageID")))
                    result = Convert.ToInt32(GetQueryStringValue("MessageID"));
                else
                    result = 0;
                return result;
            }
        }
        public Int32 AccoundIdToInvite
        {
            get
            {
                Int32 result;
                if(!string.IsNullOrEmpty(GetQueryStringValue("AccountIdToInvite")))
                {
                    result = Convert.ToInt32(GetQueryStringValue("AccountIdToInvite"));
                }
                else
                {
                    result = 0;
                }
                return result;
            }
        }
        public string SearchText
        {
            get
            {
                string result;
                if (!string.IsNullOrEmpty(GetQueryStringValue("s")))
                {
                    result = GetQueryStringValue("s");
                }
                else
                {
                    result = "";
                }
                return result;
            }
        }
        public string FriendshipRequest
        {
            get
            {
                string result;
                if(!string.IsNullOrEmpty(GetQueryStringValue("InvitationKey")))
                {
                    result = GetQueryStringValue("InvitationKey");
                }
                else
                {
                    result = "";
                }
                return result;
            }    
        }

        public string RootUrl
        {
            get
            {
                string result;

                string Port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
                if (Port == null || Port == "80" || Port == "443")
                    Port = "";
                else
                    Port = ":" + Port;

                string Protocol = HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"];
                if (Protocol == null || Protocol == "0")
                    Protocol = "http://";
                else
                    Protocol = "https://";

                result = Protocol + HttpContext.Current.Request.ServerVariables["SERVER_NAME"] +
                    Port + HttpContext.Current.Request.ApplicationPath;

                return result;
            }
        }

        public bool ShowGravatar
        {
            get
            {
                if(!string.IsNullOrEmpty(GetQueryStringValue("ShowGravatar")) && GetQueryStringValue("ShowGravatar") == "1")
                {
                    return true;
                }
                return false;
            }
        }

        public Int32 AccountID
        {
            get
            {
                if (!string.IsNullOrEmpty(GetQueryStringValue("AccountID")))
                {
                    return Convert.ToInt32(GetQueryStringValue("AccountID"));
                }
                return 0;
            }
        }

        public string CaptchaImageText
        {
            get
            {
                if(ContainsInSession("CaptchaImageText"))
                {
                    return GetFromSession("CaptchaImageText").ToString();
                }

                return null;
            }

            set
            {
                SetInSession("CaptchaImageText",value);
            }
        }

        public Account CurrentUser
        {
            get
            {
                if(ContainsInSession("CurrentUser"))
                {
                    return GetFromSession("CurrentUser") as Account;
                }

                return null;
            }
            set
            {
                SetInSession("CurrentUser", value);
            }
        }

        public string Username
        {
            get
            {
                if(ContainsInSession("Username"))
                {
                    return GetFromSession("Username").ToString();
                }

                return "";
            }

            set
            {
                SetInSession("Username",value);
            }
        }
        
        public bool LoggedIn
        {
            get
            {
                if(ContainsInSession("LoggedIn"))
                {
                    if((bool)GetFromSession("LoggedIn"))
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }   
            set
            {
                SetInSession("LoggedIn", value);
            }
        }

        public string UsernameToVerify
        {
            get
            {
                return GetQueryStringValue("a").ToString();
            }
        }

        public void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        public bool ContainsInSession(string key)
        {
            if(HttpContext.Current.Session != null && HttpContext.Current.Session[key] != null)
                return true;
            return false;
        }

        public void RemoveFromSession(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        private string GetQueryStringValue(string key)
        {
            return HttpContext.Current.Request.QueryString.Get(key);
        }

        private void SetInSession(string key, object value)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return;
            }
            HttpContext.Current.Session[key] = value;
        }

        private object GetFromSession(string key)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return null;
            }
            return HttpContext.Current.Session[key];
        }

        private void UpdateInSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }
    }
}
