using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IStatusUpdateRepository
    {
        StatusUpdate GetStatusUpdateByID(Int32 StatusUpdateID);
        List<StatusUpdate> GetStatusUpdatesByAccountID(Int32 AccountID);
        void SaveStatusUpdate(StatusUpdate statusUpdate);
        List<StatusUpdate> GetTopNStatusUpdatesByAccountID(Int32 AccountID, Int32 Number);
    }
}