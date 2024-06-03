using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IFileRepository
    {
        File GetFileByID(Int64 FileID);
        File GetFileByFileSystemName(Guid FileSystemName);
        Int64 SaveFile(File file);
        void DeleteFile(File file);
        List<File> GetFilesByFolderID(Int64 FolderID);
        void UpdateDescriptions(Dictionary<int, string> fileDescriptions);
        void DeleteFilesInFolder(Folder folder);
    }
}