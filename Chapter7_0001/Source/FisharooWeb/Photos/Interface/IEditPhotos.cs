﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Photos.Interface
{
    public interface IEditPhotos
    {
        void LoadFiles(List<File> files);
    }
}
