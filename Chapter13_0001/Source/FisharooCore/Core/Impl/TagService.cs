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
        private ITagRepository _tagRepository;
        private ISystemObjectTagRepository _systemObjectTagRepository;
        private IWebContext _webContext;
        private IConfiguration _configuration;
        private CloudSortOrder _sortOrder;

        public TagService()
        {
            _tagRepository = ObjectFactory.GetInstance<ITagRepository>();
            _systemObjectTagRepository = ObjectFactory.GetInstance<ISystemObjectTagRepository>();
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _configuration = ObjectFactory.GetInstance<IConfiguration>();

            if (_configuration.CloudSortOrder.ToLower() == "ascending")
                _sortOrder = CloudSortOrder.Ascending;
            else if (_configuration.CloudSortOrder.ToLower() == "descending")
                _sortOrder = CloudSortOrder.Descending;
            else
                _sortOrder = CloudSortOrder.Random;
        }

        public void AddTag(string TagName, int SystemObjectID, long SystemObjectRecordID)
        {
            Tag tag = _tagRepository.GetTagByName(TagName);
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
            tag = _tagRepository.SaveTag(tag);

            SystemObjectTag sysObjTag = new SystemObjectTag();
            sysObjTag.CreateDate = DateTime.Now;
            sysObjTag.CreatedByAccountID = _webContext.CurrentUser.AccountID;
            sysObjTag.CreatedByUsername = _webContext.CurrentUser.Username;
            sysObjTag.SystemObjectID = SystemObjectID;
            sysObjTag.SystemObjectRecordID = SystemObjectRecordID;
            sysObjTag.TagID = tag.TagID;

            _systemObjectTagRepository.SaveSystemObjectTag(sysObjTag);
        }

        /// <summary>
        /// Sorts the tags based on their count amount from 1 to _configuration.TagCloudLargestBaseFontSize
        /// </summary>
        /// <param name="Tags">The list of tags to calculate</param>
        /// <returns>The list of calculated tags</returns>
        public List<Tag> CalculateFontSize(List<Tag> Tags)
        {
            decimal MinimumRange;
            decimal MaximumRange;
            decimal Delta;

            //get the smallest count in this list
            MinimumRange = (Tags.OrderBy(t => t.Count).Take(1).Select(t => t.Count).FirstOrDefault());

            //get the largest count in this list
            MaximumRange = (Tags.OrderByDescending(t => t.Count).Take(1).Select(t => t.Count).FirstOrDefault());

            //determine the difference between the minimum and the maximum
            Delta = MaximumRange - MinimumRange;

            if (Tags.Count > 1)
            {
                for (int i = 0; i < Tags.Count(); i++)
                {
                    //set a working value
                    Tags[i].InitialValue = Tags[i].Count;

                    //calculate the minimum offset
                    Tags[i].MinimumOffset = Tags[i].InitialValue - MinimumRange;

                    //calculate the ranged value
                    if (Tags[i].MinimumOffset > 0 && Delta > 0)
                        Tags[i].Ranged = Tags[i].MinimumOffset / Delta;
                    else
                        Tags[i].Ranged = 0;

                    //calculate the pre calculation
                    Tags[i].PreCalculatedValue = Tags[i].Ranged*
                                                 ((_configuration.TagCloudLargestFontSize -
                                                   _configuration.TagCloudSmallestFontSize) - 1);

                    //calculate the final value
                    Tags[i].FinalCalculatedValue = Tags[i].PreCalculatedValue + 1;

                    //calculate the font size
                    Tags[i].FontSize =
                        Convert.ToInt32(Tags[i].FinalCalculatedValue + _configuration.TagCloudSmallestFontSize);
                }
            }

            //if a standard sort is not what you require, you can call Tags.Sort
            //  The Tags.Sort() method (in the Domain/Tag.cs partial class) can be 
            //  modified to use different properties to sort by

            if (_sortOrder == CloudSortOrder.Ascending) //small to tall
            {
                Tags = Tags.OrderBy(t => t.FinalCalculatedValue).ToList();
            }
            else if (_sortOrder == CloudSortOrder.Descending) //tall to small
            {
                Tags = Tags.OrderByDescending(t => t.FinalCalculatedValue).ToList();
            }
            else
            {
                Tags.ShuffleList(); //randomize!
            }

            return Tags;
        }
    }
}
