using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooWeb.Tags.Interface;
using StructureMap;

namespace Fisharoo.FisharooWeb.Tags.Presenter
{
    public class TagsPresenter
    {
        private ITags _view;
        private ISystemObjectTagRepository _systemObjectTagRepository;
        private ITagsRepository _tagRepository;
        private IWebContext _webContext;
        public TagsPresenter()
        {
            _systemObjectTagRepository = ObjectFactory.GetInstance<ISystemObjectTagRepository>();
            _tagRepository = ObjectFactory.GetInstance<ITagsRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void Init(ITags view, bool IsPostBack)
        {
            _view = view;
            _view.SetTitle(_tagRepository.GetTagByID(_webContext.TagID).Name);
            _view.LoadUI(_systemObjectTagRepository.GetSystemObjectsByTagID(_webContext.TagID));
        }
    }
}
