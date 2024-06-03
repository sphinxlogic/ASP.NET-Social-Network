using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IGroupRepository
    {
        bool CheckIfGroupPageNameExists(string PageName);
        List<Group> GetGroupsAccountIsMemberOf(Int32 AccountID);
        List<Group> GetGroupsOwnedByAccount(Int32 AccountID);
        Group GetGroupByID(Int32 GroupID);
        Group GetGroupByPageName(string PageName);
        Int32 SaveGroup(Group group);
        void DeleteGroup(Group group);
        List<Group> GetLatestGroups();
        bool IsOwner(int AccountID, int GroupID);
        Group GetGroupByForumID(int ForumID);
        void DeleteGroup(int GroupID);
    }
}