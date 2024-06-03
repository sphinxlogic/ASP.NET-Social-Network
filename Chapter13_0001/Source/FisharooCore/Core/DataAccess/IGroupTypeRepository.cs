using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IGroupTypeRepository
    {
        GroupType GetGroupTypeByID(Int32 GroupTypeID);
        List<GroupType> GetGroupTypesByGroupID(Int32 GroupID);
        Int64 SaveGroupType(GroupType groupType);
        void DeleteGroupType(GroupType groupType);
        List<GroupType> GetAllGroupTypes();        
    }
}