using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Groups.Interface
{
    public interface IManageGroup
    {
        void LoadGroup(Group group, List<GroupType> selectedTypes);
        void ShowMessage(string Message);
        void LoadGroupTypes(List<GroupType> types);
    }
}
