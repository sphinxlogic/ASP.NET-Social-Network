using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;

namespace Fisharoo.FisharooWeb.Tags.Interface
{
    public interface ITags
    {
        void LoadUI(List<SystemObjectTagWithObject> tagWithObjects);
        void SetTitle(string TagName);
    }
}
