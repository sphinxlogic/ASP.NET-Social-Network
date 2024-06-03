using System;
using System.Collections.Generic;
using System.Web;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface IFileService
    {
        string GetFullFilePathByFileID(Int64 FileID, File.Sizes FileSize);
        List<Int64> UploadPhotos(Int32 FileTypeID, Int32 AccountID, HttpFileCollection Files, Int64 AlbumID);
    }
}