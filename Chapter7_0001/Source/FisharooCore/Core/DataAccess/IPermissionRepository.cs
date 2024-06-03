using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IPermissionRepository
    {
        List<Permission> GetPermissionsByAccountID(Int32 AccountID);
        Permission GetPermissionByName(string Name);
        Permission GetPermissionByID(Int32 PermissionID);
        void SavePermission(Permission permission);
        void DeletePermission(Permission permission);
    }
}