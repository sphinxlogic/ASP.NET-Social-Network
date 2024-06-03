using System;
using System.Configuration;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Xml;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooCore.Core.Impl;
using Fisharoo.FisharooCore.Properties;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    public class Connection
    {
        public FisharooDataContext GetContext()
        {
            string connString = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(HttpContext.Current.Request.PhysicalApplicationPath + "bin/ConnectionStringToUse.xml");

                XmlNodeList xnl = doc.GetElementsByTagName("environment");
                XmlElement xe = (XmlElement) xnl[0];

                switch (xe.InnerText.ToString().ToLower())
                {
                    case "local":
                        connString = Settings.Default.FisharooConnectionStringLocal;
                        break;

                    case "development":
                        connString = Settings.Default.FisharooConnectionStringDevelopment;
                        break;

                    case "production":
                        connString = Settings.Default.FisharooConnectionStringProduction;
                        break;

                    default:
                        throw new Exception("No connection string defined in app.config!");
                }
            }
            catch(Exception e)
            {
                connString = Settings.Default.FisharooConnectionStringLocal;
            }

            FisharooDataContext fdc = new FisharooDataContext(connString);
            fdc.Log = new DebuggerWriter();
            return fdc;
        }
    }
}
