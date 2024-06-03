using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    [Pluggable("Default")]
    public class BoardService : IBoardService
    {
        private IBoardCategoryRepository _categoryRepository;
        private IBoardForumRepository _forumRepository;
        
        public BoardService()
        {
            _categoryRepository = ObjectFactory.GetInstance<IBoardCategoryRepository>();
            _forumRepository = ObjectFactory.GetInstance<IBoardForumRepository>();
        }

        public List<BoardCategory> GetCategoriesWithForums()
        {
            List<BoardCategory> categories = _categoryRepository.GetAllCategories();
            List<BoardForum> forums = _forumRepository.GetAllForums();
            for(int i = 0;i<categories.Count();i++)
            {
                categories[i].Forums = forums.Where(f => f.CategoryID == categories[i].CategoryID).ToList();
            }
            return categories;
        }
    }
}
