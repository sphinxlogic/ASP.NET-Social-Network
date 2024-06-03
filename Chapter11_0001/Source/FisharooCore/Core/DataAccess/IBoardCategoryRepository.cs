using System;
using System.Collections.Generic;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess
{
    [PluginFamily("Default")]
    public interface IBoardCategoryRepository
    {
        BoardCategory GetCategoryByCategoryID(Int32 CategoryID);
        List<BoardCategory> GetAllCategories();
        Int32 SaveCategory(BoardCategory category);
        void DeleteCategory(BoardCategory category);
        BoardCategory GetCategoryByPageName(string PageName);
    }
}