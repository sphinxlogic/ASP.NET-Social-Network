using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IGroupService
    {
        int SaveGroup(Group group);
        bool IsOwnerOrAdministrator(Int32 AccountID, Int32 GroupID);
        bool IsOwner(Int32 AccountID, Int32 GroupID);
        bool IsAdministrator(Int32 AccountID, Int32 GroupID);
        bool IsMember(Int32 AccountID, Int32 GroupID);
    }
}