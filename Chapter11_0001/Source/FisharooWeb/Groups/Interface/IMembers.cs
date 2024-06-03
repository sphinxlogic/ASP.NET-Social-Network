using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Groups.Interface
{
    public interface IMembers
    {
        void LoadData(List<Account> Members, List<Account> MembersToApprove);
        void ShowMessage(string Message);
        void SetButtonsVisibility(bool Visible);
    }
}
