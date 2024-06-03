using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFriendshipDefinitionRepository
    {
        FriendshipDefinition GetFriendshipDefinitionByID(Int32 FriendshipDefinitionID);
        List<FriendshipDefinition> GetFriendshipDefinitionsByFriendID(Int32 FriendID);
        void SaveFriendshipDefinition(FriendshipDefinition friendshipDefinition);
        void DeleteFriendshipDefinintion(FriendshipDefinition friendshipDefinition);
    }
}