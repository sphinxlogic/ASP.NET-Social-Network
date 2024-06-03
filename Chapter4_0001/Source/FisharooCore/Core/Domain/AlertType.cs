using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public partial class AlertType
    {
        public enum AlertTypes
        {
            AccountCreated = 1,
            ProfileCreated = 2,
            AccountModified = 3,
            ProfileModified = 4,
            NewAvatar = 5,
            AddedFriend = 6,
            AddedPicture = 7
        }
    }
}
