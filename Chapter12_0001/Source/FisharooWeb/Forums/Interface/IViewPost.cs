using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Forums.Interface
{
    public interface IViewPost
    {
        void LoadData(BoardPost Thread, List<BoardPost> Posts);
    }
}
