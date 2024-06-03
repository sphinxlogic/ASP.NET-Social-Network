using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IGroupForumRepository
    {
        void SaveGroupForum(GroupForum groupForum);
        void DeleteGroupForum(GroupForum groupForum);
        int GetGroupIdByForumID(int ForumID);
        void DeleteGroupForum(int ForumID, int GroupID);
    }
}