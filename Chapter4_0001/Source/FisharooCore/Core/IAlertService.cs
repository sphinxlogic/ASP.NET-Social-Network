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
        void AddAccountModifiedAlert();
        void AddProfileCreatedAlert();
        void AddProfileModifiedAlert();
        void AddNewAvatarAlert();
        List<Alert> GetAlertsByAccountID(Int32 AccountID);
    }
}