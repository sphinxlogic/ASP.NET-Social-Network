using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class ProfileAttributeRepository : IProfileAttributeRepository
    {
        private Connection conn;
        public ProfileAttributeRepository()
        {
            conn = new Connection();
        }

        public ProfileAttributeType GetProfileAttributeTypeByID(Int32 profileAttributeTypeID)
        {
            ProfileAttributeType result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.ProfileAttributeTypes.Where(pat => pat.ProfileAttributeTypeID == profileAttributeTypeID).FirstOrDefault();
            }
            return result;
        }

        public List<ProfileAttributeType> GetProfileAttributeTypes()
        {
            List<ProfileAttributeType> result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<ProfileAttributeType> types = from t in dc.ProfileAttributeTypes
                        orderby t.SortOrder
                        select t;
                result = types.ToList();
            }

            return result;
        }

        public void AddProfileAttributes(params ProfileAttribute[] attributes)
        {
            foreach (ProfileAttribute attribute in attributes)
            {
                SaveProfileAttribute(attribute);
            }
        }

        public void SaveProfileAttribute(ProfileAttribute attribute)
        {
            using (FisharooDataContext dc = conn.GetContext())
            {
                if (attribute.ProfileAttributeID > 0)
                {
                    dc.ProfileAttributes.Attach(attribute, true);
                }
                else
                {
                    dc.ProfileAttributes.InsertOnSubmit(attribute);
                }
                dc.SubmitChanges();
            }
        }

        public List<ProfileAttribute> GetProfileAttributesByProfileID(Int32 ProfileID)
        {
            List<ProfileAttribute> list = new List<ProfileAttribute>();

            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<ProfileAttribute> result = from pa in dc.ProfileAttributes
                                                        join pat in dc.ProfileAttributeTypes
                                                        on pa.ProfileAttributeTypeID equals pat.ProfileAttributeTypeID
                                                        orderby pat.SortOrder
                                                        where pa.ProfileID == ProfileID
                                                        select pa;
                list = result.ToList();
            }
            
            return list;
        }
    }
}
