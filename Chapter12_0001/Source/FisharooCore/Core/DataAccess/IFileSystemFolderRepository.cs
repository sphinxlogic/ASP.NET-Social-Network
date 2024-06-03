using System;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFileSystemFolderRepository
    {
        FileSystemFolder GetFileSystemFolderByID(Int32 FileSystemFolderID);
        void SaveFileSystemFolder(FileSystemFolder FileSystemFolder);
        void DeleteFileSystemFolder(FileSystemFolder FileSystemFolder);
    }
}