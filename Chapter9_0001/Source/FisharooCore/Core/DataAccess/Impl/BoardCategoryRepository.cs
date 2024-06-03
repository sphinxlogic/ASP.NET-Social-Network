using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class BoardCategoryRepository : IBoardCategoryRepository
    {
        private Connection _conn;
        public BoardCategoryRepository()
        {
            _conn = new Connection();
        }

        public BoardCategory GetCategoryByCategoryID(Int32 CategoryID)
        {
            BoardCategory result;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                BoardCategory bc = dc.BoardCategories.Where(c => c.CategoryID == CategoryID).FirstOrDefault();
                result = bc;
            }
            return result;
        }

        public BoardCategory GetCategoryByPageName(string PageName)
        {
            BoardCategory category;
            using(FisharooDataContext dc = _conn.GetContext())
            {
                category = dc.BoardCategories.Where(bc => bc.PageName == PageName).FirstOrDefault();
            }
            return category;
        }

        public List<BoardCategory> GetAllCategories()
        {
            List<BoardCategory> result;
            using (FisharooDataContext dc = _conn.GetContext())
            {
                IEnumerable<BoardCategory> categories = dc.BoardCategories;
                result = categories.ToList();
            }
            return result;
        }

        public Int32 SaveCategory(BoardCategory category)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                if(category.CategoryID > 0)
                {
                    dc.BoardCategories.Attach(category, true);
                }
                else
                {
                    dc.BoardCategories.InsertOnSubmit(category);
                }
                dc.SubmitChanges();
            }
            return category.CategoryID;
        }

        public void DeleteCategory(BoardCategory category)
        {
            using(FisharooDataContext dc = _conn.GetContext())
            {
                dc.BoardCategories.Attach(category, true);
                dc.BoardCategories.DeleteOnSubmit(category);
                dc.SubmitChanges();
            }
        }
    }
}
