﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fisharoo.FisharooWeb.Forums.Interface
{
    public interface IPost
    {
        void SetDisplay(bool IsThread);
        void SetErrorMessage(string Message);
    }
}
