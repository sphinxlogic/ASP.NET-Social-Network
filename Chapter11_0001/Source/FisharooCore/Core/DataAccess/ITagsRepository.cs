using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ITagsRepository
    {
        Tag GetTagByName(string Name);
        Tag GetTagByID(int TagID);
        List<Tag> GetTagsBySystemObject(int SystemObjectID, int TagsToTake);
        List<Tag> GetTagsBySystemObjectAndRecordID(int SystemObjectID, long SystemObjectRecordID);
        Tag SaveTag(Tag tag);
        void DeleteTag(Tag tag);
        List<Tag> GetTagsGlobal(int TagsToTake);
    }
}