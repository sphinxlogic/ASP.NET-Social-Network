using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFriendInvitationRepository
    {
        List<FriendInvitation> GetFriendInvitationsByAccountID(Int32 AccountID);
        void SaveFriendInvitation(FriendInvitation friendInvitation);
        FriendInvitation GetFriendInvitationByGUID(Guid guid);
        void CleanUpFriendInvitationsForThisEmail(FriendInvitation friendInvitation);
    }
}