using System;
using System.Data;
using System.Configuration;
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
using Fisharoo.FisharooWeb.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Presenter
{
    public class SearchPresenter
    {
        private ISearch _view;
        private IAccountRepository _accountRepository;
        private IRedirector _redirector;

        public void Init(ISearch view)
        {
            _view = view;
            _accountRepository = ObjectFactory.GetInstance<IAccountRepository>();
            _redirector = ObjectFactory.GetInstance<IRedirector>();
        }

        public void PerformSearch(string SearchText)
        {
            _view.LoadAccounts(_accountRepository.SearchAccounts(SearchText));
        }
    }
}
