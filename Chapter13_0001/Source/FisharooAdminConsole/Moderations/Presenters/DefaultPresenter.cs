using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fisharoo.FisharooAdminConsole.Moderations.Interface;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooAdminConsole.Moderations.Presenters
{
    public class DefaultPresenter
    {
        private IDefault _view;
        private IModerationRepository _moderationRepository;
        private IGagRepository _gagRepository;
        private IWebContext _webContext;
        public DefaultPresenter()
        {
            _moderationRepository = ObjectFactory.GetInstance<IModerationRepository>();
            _gagRepository = ObjectFactory.GetInstance<IGagRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(IDefault view, bool IsPostBack)
        {
            _view = view;
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void GagUserUntil(int AccountID, string AccountUsername, DateTime GagTillDate, string Reason)
        {
            Gag gag = new Gag();
            gag.AccountID = AccountID;
            gag.CreateDate = DateTime.Now;
            gag.AccountUsername = AccountUsername;
            gag.GagUntilDate = GagTillDate;
            gag.Reason = Reason;
            gag.GaggedByAccountID = _webContext.CurrentUser.AccountID;
            _gagRepository.SaveGag(gag);
        }

        public void LoadData()
        {
            _view.LoadData(_moderationRepository.GetModerationsGlobal());
        }

        public void SaveModerationResults(List<ModerationResult> results)
        {
            _moderationRepository.SaveModerationResults(results, _webContext.CurrentUser.AccountID, _webContext.CurrentUser.Username);
            _view.ClearData();
            LoadData();
        }
    }
}
