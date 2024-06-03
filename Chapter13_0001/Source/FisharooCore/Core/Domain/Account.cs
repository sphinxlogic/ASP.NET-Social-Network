using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooCore.Core.Impl;

namespace Fisharoo.FisharooCore.Core.Domain
{
    [Serializable]
    public partial class Account
    {
        private List<Permission> _permissions = new List<Permission>();
        private Profile _profile;

        public Profile Profile
        {
            get { return _profile; }
            set { _profile = value; }
        }

        public List<Permission> Permissions
        {
            get{ return _permissions; }
        }

        public void AddPermission(Permission permission)
        {
            _permissions.Add(permission);
        }

        public bool HasPermission(string Name)
        {
            foreach (Permission p in _permissions)
            {
                if (p.Name == Name)
                    return true;
            }
            return false;
        }
    }
}
