using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooAdminConsole.Moderations.Interface
{
    public interface IDefault
    {
        void LoadData(List<Moderation> moderations);
        void ClearData();
    }
}
