using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public partial class Friend
    {
        public List<FriendshipDefinition> FriendshipDefinitions { get; set; }
    }
}
