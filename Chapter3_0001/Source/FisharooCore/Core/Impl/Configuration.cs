using System;
using System.Configuration;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class Configuration : IConfiguration
    {
        public string SiteName
        {
            get{ return getAppSetting(typeof(string),"SiteName").ToString();}
        }

        public string RootURL
        {
            get { return getAppSetting(typeof(string), "RootURL").ToString(); }
        }

        private static object getAppSetting(Type expectedType, string key)
        {
            string value = ConfigurationManager.AppSettings.Get(key);
            if (value == null)
            {
                throw new Exception(string.Format("AppSetting: {0} is not configured.", key));
            }

            try
            {
                if (expectedType.Equals(typeof(int)))
                {
                    return int.Parse(value);
                }

                if (expectedType.Equals(typeof(string)))
                {
                    return value;
                }

                throw new Exception("Type not supported.");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Config key:{0} was expected to be of type {1} but was not.", key, expectedType),
                                    ex);
            }
        }
    }
}
