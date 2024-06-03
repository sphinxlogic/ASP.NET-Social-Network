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
    public class TagService : ITagService
    {
        private ITagsRepository _tagsRepository;
        private ISystemObjectTagRepository _systemObjectTagRepository;
        private IWebContext _webContext;

        public TagService()
        {
            _tagsRepository = ObjectFactory.GetInstance<ITagsRepository>();
            _systemObjectTagRepository = ObjectFactory.GetInstance<ISystemObjectTagRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
        }

        public void AddTag(string TagName, int SystemObjectID, long SystemObjectRecordID)
        {
            Tag tag = _tagsRepository.GetTagByName(TagName);
            if (tag == null)
            {
                tag = new Tag();
                tag.CreateDate = DateTime.Now;
                tag.Name = TagName;
                tag.Count = 1;
            }
            else
            {
                tag.Count += 1;
            }
            tag = _tagsRepository.SaveTag(tag);

            SystemObjectTag sysObjTag = new SystemObjectTag();
            sysObjTag.CreateDate = DateTime.Now;
            sysObjTag.CreatedByAccountID = _webContext.CurrentUser.AccountID;
            sysObjTag.CreatedByUsername = _webContext.CurrentUser.Username;
            sysObjTag.SystemObjectID = SystemObjectID;
            sysObjTag.SystemObjectRecordID = SystemObjectRecordID;
            sysObjTag.TagID = tag.TagID;

            _systemObjectTagRepository.SaveSystemObjectTag(sysObjTag);
        }
    }
}
