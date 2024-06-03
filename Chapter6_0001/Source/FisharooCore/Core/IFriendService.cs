using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IFriendService
    {
        void CreateFriendFromFriendInvitation(Guid InvitationKey, Account InvitationTo);
        bool IsFriend(Account account, Account accountBeingViewed);
    }
}