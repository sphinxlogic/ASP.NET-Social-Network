using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IAlertService
    {
        void AddAccountCreatedAlert();
        void AddAccountModifiedAlert(Account modifiedAccount);
        void AddProfileCreatedAlert();
        void AddProfileModifiedAlert();
        void AddNewAvatarAlert();
        List<Alert> GetAlertsByAccountID(Int32 AccountID);
        void AddFriendAddedAlert(Account FriendRequestFrom, Account FriendRequestTo);
        void AddFriendRequestAlert(Account FriendRequestFrom, Account FriendRequestTo, Guid requestGuid, string Message);
        void AddStatusUpdateAlert(StatusUpdate statusUpdate);
    }
}