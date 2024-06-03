using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class AlertService : IAlertService
    {
        private IUserSession _userSession;
        private IAlertRepository _alertRepository;
        private IWebContext _webContext;

        private Account account;
        private Alert alert;
        private string alertMessage;
        private string[] tags = {"[rootUrl]"};
        public AlertService()
        {
            _userSession = ObjectFactory.GetInstance<IUserSession>();
            _alertRepository = ObjectFactory.GetInstance<IAlertRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        private void Init()
        {
            account = _userSession.CurrentUser;

            alert = new Alert();
            alert.AccountID = account.AccountID;
            alert.CreateDate = DateTime.Now;
        }

        public void AddAccountCreatedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) + " just signed up!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(account.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int)AlertType.AlertTypes.AccountCreated;
            SaveAlert(alert);
        }

        public void AddAccountModifiedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " modified their account.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.AccountModified;
            SaveAlert(alert);
        }

        public void AddProfileCreatedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " just created their profile!</div>";
            alertMessage += "<div class=\"AlertRow\">" + GetSendMessageUrl(account.AccountID) + "</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.ProfileCreated;
            SaveAlert(alert);
        }

        public void AddProfileModifiedAlert()
        {
            Init();
            alertMessage = "<div class=\"AlertHeader\">" + GetProfileUrl(account.Username) +
                           " modified their profile.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.ProfileModified;
            SaveAlert(alert);
        }

        public void AddNewAvatarAlert()
        {
            Init();
            alertMessage =
                "<div class=\"AlertHeader\"><img src=\"[rootUrl]images/ProfileAvatar/ProfileImage.aspx?AccountID=" +
                account.AccountID.ToString() + "\" width=\"100\" height=\"100\" align=\"absmiddle\">" + GetProfileUrl(account.Username) + " added a new avatar.</div>";
            alert.Message = alertMessage;
            alert.AlertTypeID = (int) AlertType.AlertTypes.NewAvatar;
            SaveAlert(alert);
        }

        public List<Alert> GetAlertsByAccountID(Int32 AccountID)
        {
            List<Alert> result = new List<Alert>();
            List<Alert> alerts = _alertRepository.GetAlertsByAccountID(AccountID);
            foreach (Alert alert in alerts)
            {
                foreach (string s in tags)
                {
                    switch(s)
                    {
                        case "[rootUrl]":
                            alert.Message = alert.Message.Replace("[rootUrl]", _webContext.RootUrl);
                            result.Add(alert);
                            break;
                    }
                }
            }

            return result;
        }

        private void SaveAlert(Alert alert)
        {
            _alertRepository.SaveAlert(alert);
        }

        private string GetProfileUrl(string Username)
        {
            return "<a href=\"[rootUrl]" + account.Username + "\">" + account.Username + "</a>";
        }

        private string GetSendMessageUrl(Int32 AccountID)
        {
            return "Click here to send message";
        }
    }
}
