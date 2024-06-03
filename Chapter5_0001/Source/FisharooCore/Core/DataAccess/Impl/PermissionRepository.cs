using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class PermissionRepository : IPermissionRepository
    {
        private Connection conn;
        public PermissionRepository()
        {
            conn = new Connection();    
        }

        public List<Permission> GetPermissionsByAccountID(Int32 AccountID)
        {
            List<Permission> returnPermissions = new List<Permission>();

            using (FisharooDataContext dc = conn.GetContext())
            {
                var permissions =   from p in dc.Permissions
                                    join ap in dc.AccountPermissions on p.PermissionID equals ap.PermissionID
                                    join a in dc.Accounts on ap.AccountID equals a.AccountID
                                    where a.AccountID == AccountID
                                    select p;

                foreach (Permission permission in permissions)
                {
                    returnPermissions.Add(permission);
                }
            }

            return returnPermissions;
        }

        public Permission GetPermissionByName(string Name)
        {
            Permission result;

            using (FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Permissions.Where(p => p.Name == Name).FirstOrDefault();
            }

            return result;
        }

        public Permission GetPermissionByID(Int32 PermissionID)
        {
            Permission result;

            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.Permissions.Where(p => p.PermissionID == PermissionID).FirstOrDefault();
            }

            return result;
        }

        public void SavePermission(Permission permission)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(permission.PermissionID > 0)
                {
                    dc.Permissions.Attach(permission,true);
                }
                else
                {
                    dc.Permissions.InsertOnSubmit(permission);
                }
                dc.SubmitChanges();
            }
        }

        public void DeletePermission(Permission permission)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.Permissions.DeleteOnSubmit(permission);
                dc.SubmitChanges();
            }
        }
    }
}
