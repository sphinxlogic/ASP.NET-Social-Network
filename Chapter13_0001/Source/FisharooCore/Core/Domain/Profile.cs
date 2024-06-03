using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    [Serializable]
    public partial class Profile
    {
        public List<ProfileAttribute> Attributes { get; set;}
        public LevelOfExperienceType LevelOfExperienceType { get; set; }
    }
}
