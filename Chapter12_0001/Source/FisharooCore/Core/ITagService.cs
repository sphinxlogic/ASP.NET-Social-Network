using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface ITagService
    {
        void AddTag(string TagName, int SystemObjectID, long SystemObjectRecordID);
        List<Tag> CalculateFontSize(List<Tag> Tags);
    }
}
