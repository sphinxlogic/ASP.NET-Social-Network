﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public partial class Group
    {
        public List<GroupTypes> Types { get; set; }
        public List<Folder> Folders { get; set; }
        public List<BoardForum> Forums { get; set; }
        public List<Account> Members { get; set; }
        public List<Group> RelatedGroups { get; set;}
    }
}
