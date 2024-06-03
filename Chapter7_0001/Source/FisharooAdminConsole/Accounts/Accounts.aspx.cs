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
using Fisharoo.FisharooCore.Core.DataAccess;
using StructureMap;

namespace Fisharoo.FisharooAdminConsole.Account
{
    public partial class Accounts : System.Web.UI.Page
    {
        private IAccountRepository _accountRepository;
        private Int32 _pageNum;
        private Int32 _numberOfRecords;

        protected void Page_Load(object sender, EventArgs e)
        {
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();

            if (Request.QueryString["pn"] != null)
                _pageNum = Convert.ToInt32(Request.QueryString["pn"]);
            else
                _pageNum = 1;

            _numberOfRecords = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfPagingRecordsToDisplay"]);

            gvAccounts.DataSource = _accountRepository.GetAllAccounts(_pageNum);
            gvAccounts.DataBind();

            ConfigureDisplay();
        }

        private void ConfigureDisplay()
        {
            if (_pageNum == 1)
                btnPrevious.Enabled = false;
            else
                btnPrevious.Enabled = true;

            if (gvAccounts.Rows.Count == _numberOfRecords)
                btnNext.Enabled = true;
            else
                btnNext.Enabled = false;
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_pageNum > 1)
                Response.Redirect("~/Account/Accounts.aspx?pn=" + (_pageNum - 1).ToString());
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (gvAccounts.Rows.Count == _numberOfRecords)
                Response.Redirect("~/Account/Accounts.aspx?pn=" + (_pageNum + 1).ToString());
        }
    }
}
