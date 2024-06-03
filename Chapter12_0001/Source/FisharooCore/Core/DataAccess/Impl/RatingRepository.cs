using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class RatingRepository : IRatingRepository
    {
        private Connection conn;
        public RatingRepository()
        {
            conn = new Connection();
        }

        public bool HasRatedBefore(int SystemObjectID, long SystemObjectRecordID, int AccountID)
        {
            bool result = false;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (dc.Ratings.Where(r => r.SystemObjectID == SystemObjectID && 
                    r.SystemObjectRecordID == SystemObjectRecordID && 
                    r.CreatedByAccountID == AccountID).Count() > 0)
                    result = true;
            }
            return result;
        }

        public int GetCurrentRating(int SystemObjectID, long SystemObjectRecordID)
        {
            double result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                if (dc.Ratings.Where(r => r.SystemObjectID == SystemObjectID && r.SystemObjectRecordID == SystemObjectRecordID).Count() > 0)
                    result =
                        dc.Ratings.Where(
                            r => r.SystemObjectID == SystemObjectID && r.SystemObjectRecordID == SystemObjectRecordID).
                            Select(r => r.Score).Average();
                else
                    result = 0;
            }
            return Convert.ToInt32(result);
        }

        public void SaveRatings(List<Rating> ratings)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                //get a list of items that have been rated before
                List<long> previouslyRatedSystemObjectRecordIDs = dc.Ratings.Where(r => r.CreatedByAccountID == ratings[0].CreatedByAccountID).Select(r=>r.SystemObjectRecordID).ToList();

                foreach (Rating rating in ratings)
                {
                    //be sure that this user has not already rated this particular system object before
                    if (!previouslyRatedSystemObjectRecordIDs.Contains(rating.SystemObjectRecordID))
                        dc.Ratings.InsertOnSubmit(rating);
                }
                dc.SubmitChanges();
            }
        }

        public long SaveRating(Rating rating)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (rating.RatingID > 0)
                {
                    dc.Ratings.Attach(rating, true);
                }
                else
                {
                    dc.Ratings.InsertOnSubmit(rating);
                }
                dc.SubmitChanges();
            }
            return rating.RatingID;
        }

        public void DeleteRating(Rating rating)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Ratings.Attach(rating, true);
                dc.Ratings.DeleteOnSubmit(rating);
                dc.SubmitChanges();
            }
        }
    }
}
