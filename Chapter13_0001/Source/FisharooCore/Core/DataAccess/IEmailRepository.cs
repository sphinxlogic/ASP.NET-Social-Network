using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IEmailRepository
    {
        List<pr_MailQueue_SwapReceivingAndWorking_GetWorkingResult> GetMailQueueToProcess();
        void MoveMailQueueWorkingToHistory();
        void Save(MailQueue_Receiving MailQueue);
    }
}