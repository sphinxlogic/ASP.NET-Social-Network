using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IRatingRepository
    {
        void SaveRatings(List<Rating> ratings);
        long SaveRating(Rating rating);
        void DeleteRating(Rating rating);
        int GetCurrentRating(int SystemObjectID, long SystemObjectRecordID);
        bool HasRatedBefore(int SystemObjectID, long SystemObjectRecordID, int AccountID);
    }
}