using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface ICommentsRepository
    {
        Comment GetCommentByID(long CommentID);
        List<Comment> GetCommentsBySystemObject(int SystemObjectID, long SystemObjectRecordID);
        long SaveComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}