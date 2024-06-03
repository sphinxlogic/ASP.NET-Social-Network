using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.UserControls.Interfaces
{
    public interface ITags
    {
        void ShowTagCloud(bool Visible);
        void ShowTagBox(bool Visible);
        int SystemObjectID { get; set; }
        long SystemObjectRecordID { get; set; }
        TagState Display { get; set; }
        void AddTagsToTagCloud(Tag tag);
        void ClearTagCloud();
    }
}
