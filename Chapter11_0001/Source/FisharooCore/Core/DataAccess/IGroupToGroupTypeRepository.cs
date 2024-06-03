using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IGroupToGroupTypeRepository
    {
        void SaveGroupToGroupType(GroupToGroupType groupToGroupType);
        void DeleteGroupToGroupType(GroupToGroupType groupToGroupType );
        void SaveGroupTypesForGroup(List<long> SelectedGroupTypeIDs, int GroupID);
    }
}