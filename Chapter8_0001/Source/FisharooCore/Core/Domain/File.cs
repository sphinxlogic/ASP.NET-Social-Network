﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooCore.Core.Domain
{
    public partial class File
    {
        public enum Types
        {
            JPG = 1,
            GIF = 2,
            WAV = 3,
            MP3 = 4,
            WMV = 5,
            FLV = 6,
            JPEG = 7,
            SWF = 8
        }

        public string Extension { get; set; }
    }
}
