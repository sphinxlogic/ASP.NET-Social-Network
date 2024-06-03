using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IAlertRepository
    {
        List<Alert> GetAlertsByAccountID(Int32 AccountID);
        void SaveAlert(Alert alert);
        void DeleteAlert(Alert alert);
    }
}