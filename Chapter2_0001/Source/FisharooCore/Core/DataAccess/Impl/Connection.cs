using System.Configuration;
using System.Linq;
using System.Data.Linq;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    public class Connection
    {
        public static FisharooDataContext GetContext()
        {
            FisharooDataContext fdc = new FisharooDataContext(ConfigurationManager.ConnectionStrings["Fisharoo"].ToString());
            return fdc;
        }
    }
}
