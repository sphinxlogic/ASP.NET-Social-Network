using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess.Impl;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class FileSystemFolderRepository : IFileSystemFolderRepository
    {
        private Connection _conn;
        public FileSystemFolderRepository()
        {
            _conn = new Connection();
        }

        public FileSystemFolder GetFileSystemFolderByID(Int32 FileSystemFolderID)
        {
            throw new Exception("GetFileSystemFolderByID is not implemented!");
        }

        public void SaveFileSystemFolder(FileSystemFolder FileSystemFolder)
        {
            throw new Exception("SaveFileSystemFolder is not implemented!");
        }

        public void DeleteFileSystemFolder(FileSystemFolder FileSystemFolder)
        {
            throw new Exception("DeleteFileSystemFolder is not implemented!");
        }
    }
}