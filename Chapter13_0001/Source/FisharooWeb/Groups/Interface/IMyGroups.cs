using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Groups.Interface
{
    public interface IMyGroups
    {
        void LoadData(List<Group> groups);
        void ShowMessage(string message);
    }
}
