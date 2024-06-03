using System;
using System.Web;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class WebContext : IWebContext
    {
        //CHAPTER 4
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

        //CHAPTER 4
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

        //CHAPTER 4
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

        //CHAPTER 3
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

        //CHAPTER 3
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

        //CHAPTER 3
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
        
        //CHAPTER 3
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

        //CHAPTER 3
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
