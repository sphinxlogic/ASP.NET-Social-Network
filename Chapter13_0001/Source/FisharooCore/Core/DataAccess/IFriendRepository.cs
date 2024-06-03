using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFriendRepository
    {
        Friend GetFriendByID(Int32 FriendID);
        List<Friend> GetFriendsByAccountID(Int32 AccountID);
        List<Account> GetFriendsAccountsByAccountID(Int32 AccountID);
        void SaveFriend(Friend friend);
        void DeleteFriend(Friend friend);
        void DeleteFriendByID(Int32 AccountIDToRemoveFriendFrom, Int32 FriendIDToRemove);
    }
}