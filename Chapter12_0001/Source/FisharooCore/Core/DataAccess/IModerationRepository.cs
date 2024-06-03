using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IModerationRepository
    {
        List<Moderation> GetModerationsByAccountID(int AccountID);
        List<Moderation> GetModerationsGlobal();
        Moderation SaveModeration(Moderation moderation);
        void DeleteModeration(Moderation moderation);
        bool HasFlaggedThisAlready(int AccountID, int SystemObjectID, long SystemObjectRecordID);
        void SaveModerationResults(List<ModerationResult> results, int ActionByAccountID, string ActionByUsername);
    }
}