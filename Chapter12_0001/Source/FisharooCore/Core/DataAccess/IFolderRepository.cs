using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFolderRepository
    {
        Folder GetFolderByID(Int64 FolderID);
        Int64 SaveFolder(Folder folder);
        void DeleteFolder(Folder folder);
        List<Folder> GetFoldersByAccountID(Int32 AccountID);
        List<Folder> GetFriendsFolders(List<Friend> Friends);
    }
}