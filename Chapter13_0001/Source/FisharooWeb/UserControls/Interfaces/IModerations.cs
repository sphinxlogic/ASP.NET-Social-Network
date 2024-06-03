using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooWeb.UserControls.Interfaces
{
    public interface IModerations
    {
        int SystemObjectID { get; set; }
        long SystemObjectRecordID { get; set; }
        bool ShowFlagThis { set; }
    }
}
