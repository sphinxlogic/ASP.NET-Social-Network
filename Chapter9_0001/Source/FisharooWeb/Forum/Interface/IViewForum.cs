using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Forums.Interface
{
    public interface IViewForum
    {
        void LoadDisplay(List<BoardPost> Threads, string CategoryPageName, string ForumPageName, Int32 ForumID);
    }
}
