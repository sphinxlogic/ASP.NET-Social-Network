using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooCore.Core.Domain;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    public class TagsPresenter
    {
        private ITags _view;
        private ITagService _tagService;
        private IWebContext _webContext;
        private ITagsRepository _tagRepository;

        public TagsPresenter()
        {
            _tagService = ObjectFactory.GetInstance<ITagService>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _tagRepository = ObjectFactory.GetInstance<ITagsRepository>();
        }

        public void Init(ITags view, bool IsPostBack)
        {
            _view = view;
            DetermineClientState();
        }

        public void DetermineClientState()
        {
            if (_webContext.CurrentUser != null && _view.Display == TagState.ShowCloud)
            {
                _view.ShowTagCloud(true);
                BuildRecordTagCloud();
            }
            else if (_webContext.CurrentUser != null && _view.Display == TagState.ShowCloudAndTagBox)
            {
                _view.ShowTagBox(true);
                _view.ShowTagCloud(true);
                BuildRecordTagCloud();
            }
            else if (_webContext.CurrentUser == null && _view.Display == TagState.ShowCloudAndTagBox)
            {
                _view.ShowTagBox(false);
                _view.ShowTagCloud(true);
                BuildRecordTagCloud();
            }
            else if (_view.Display == TagState.ShowCloud)
            {
                _view.ShowTagBox(true);
            }
            else if (_view.Display == TagState.ShowParentCloud)
            {
                _view.ShowTagCloud(true);
                _view.ShowTagBox(false);
                BuildParentTagCloud();
            }
            else if (_view.Display == TagState.ShowGlobalCloud)
            {
                _view.ShowTagCloud(true);
                _view.ShowTagBox(false);
                BuildGlobalTagCloud();
            }
            else
            {
                _view.ShowTagBox(false);
                _view.ShowTagCloud(false);
            }
        }

        public void BuildGlobalTagCloud()
        {
            List<Tag> tags = _tagRepository.GetTagsGlobal(30);
            foreach (Tag tag in tags)
            {
                _view.AddTagsToTagCloud(tag);
            }
        }
        public void BuildParentTagCloud()
        {
            List<Tag> tags = _tagRepository.GetTagsBySystemObject(_view.SystemObjectID, 30);
            foreach (Tag tag in tags)
            {
                _view.AddTagsToTagCloud(tag);
            }
        }

        public void BuildRecordTagCloud()
        {
            List<Tag> tags = _tagRepository.GetTagsBySystemObjectAndRecordID(_view.SystemObjectID, _view.SystemObjectRecordID);
            foreach (Tag tag in tags)
            {
                _view.AddTagsToTagCloud(tag);
            }
        }

        public void btnTag_Click(string TagName)
        {
            _tagService.AddTag(TagName, _view.SystemObjectID, _view.SystemObjectRecordID);
            if (_view.Display == TagState.ShowCloud || _view.Display == TagState.ShowCloudAndTagBox)
            {
                _view.ClearTagCloud();
                BuildRecordTagCloud();
            }
        }
    }
}
