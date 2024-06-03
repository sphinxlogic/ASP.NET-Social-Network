using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFriendshipDefinitionTypeRepository
    {
        FriendshipDefinitionType GetFriendshipDefinitionTypeByID(Int32 FriendshipDefinitionTypeID);
    }
}