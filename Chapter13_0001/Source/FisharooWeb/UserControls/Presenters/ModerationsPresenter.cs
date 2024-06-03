using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    public class ModerationsPresenter
    {
        private IModerations _view;
        private IWebContext _webContext;
        private IModerationRepository _moderationRepository;
 
        public ModerationsPresenter()
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _moderationRepository = ObjectFactory.GetInstance<IModerationRepository>();
        }

        public void Init(IModerations view, bool IsPostBack)
        {
            _view = view;

            if (_webContext.CurrentUser == null)
                _view.ShowFlagThis = false;
            else if (_moderationRepository.HasFlaggedThisAlready(_webContext.CurrentUser.AccountID, _view.SystemObjectID, _view.SystemObjectRecordID))
                _view.ShowFlagThis = false;
            else
                _view.ShowFlagThis = true;
        }

        public void SaveModeration(int SystemObjectID, long SystemObjectRecordID)
        {
            if (_webContext.CurrentUser != null)
            {
                Moderation moderation = new Moderation();
                moderation.AccountID = _webContext.CurrentUser.AccountID;
                moderation.AccountUsername = _webContext.CurrentUser.Username;
                moderation.CreateDate = DateTime.Now;
                moderation.SystemObjectID = _view.SystemObjectID;
                moderation.SystemObjectRecordID = _view.SystemObjectRecordID;
                _moderationRepository.SaveModeration(moderation);
            }
            _view.ShowFlagThis = false;
        }
    }
}
