using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Photos.Interface
{
    public interface IViewAlbum
    {
        void LoadPhotos(List<File> files);
        void LoadAlbumDetails(Folder folder);
    }
}
