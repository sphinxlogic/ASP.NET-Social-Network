using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.UserControls.Interfaces
{
    public interface IRatings
    {
        int SystemObjectID { get; set; }
        long SystemObjectRecordID { get; set; }
        void LoadOptions(List<SystemObjectRatingOption> Options);
        void CanSetRating(bool Visible);
        void SetCurrentRating(int CurrentRating);
    }
}
