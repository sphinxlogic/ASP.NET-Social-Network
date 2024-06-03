using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.UserControls.Interfaces
{
    public interface IComments
    {
        int SystemObjectID { get; set; }
        long SystemObjectRecordID { get; set; }
        void ShowCommentBox(bool IsVisible);
        void LoadComments(List<Comment> comments);
        void ClearComments();
    }
}
