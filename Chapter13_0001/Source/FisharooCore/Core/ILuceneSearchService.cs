using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Impl;
using StructureMap;

namespace Fisharoo.FisharooCore.Core
{
    [PluginFamily("Default")]
    public interface ILuceneSearchService
    {
        event EventHandler RecordAddedEvent;
        List<SearchResult> Search(string InputText);
        void BuildIndexesThread();
    }
}