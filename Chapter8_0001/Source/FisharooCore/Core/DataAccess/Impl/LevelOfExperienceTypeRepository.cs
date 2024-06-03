using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fisharoo.FisharooCore.Core.Domain;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class LevelOfExperienceTypeRepository : ILevelOfExperienceTypeRepository
    {
        private Connection conn;
        public LevelOfExperienceTypeRepository()
        {
            conn = new Connection();
        }

        public List<LevelOfExperienceType> GetAllLevelOfExperienceTypes()
        {
            List<LevelOfExperienceType> types = new List<LevelOfExperienceType>();
            using(FisharooDataContext dc = conn.GetContext())
            {
                IEnumerable<LevelOfExperienceType> result = dc.LevelOfExperienceTypes.OrderBy(l => l.SortOrder);
                types = result.ToList();
            }
            return types;
        }

        public LevelOfExperienceType GetLevelOfExperienceTypeByID(int LevelOfExperienceTypeID)
        {
            LevelOfExperienceType result;
            using(FisharooDataContext dc = conn.GetContext())
            {
                result = dc.LevelOfExperienceTypes.Where(l => l.LevelOfExperienceTypeID == LevelOfExperienceTypeID).FirstOrDefault();
            }
            return result;
        }

        public void SaveLevelOfExperienceType(LevelOfExperienceType levelOfExperienceType)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                if(levelOfExperienceType.LevelOfExperienceTypeID > 0)
                {
                    dc.LevelOfExperienceTypes.Attach(levelOfExperienceType);
                }
                else
                {
                    dc.LevelOfExperienceTypes.InsertOnSubmit(levelOfExperienceType);
                }
                dc.SubmitChanges();
            }
        }

        public void DeleteLevelOfExperienceType(LevelOfExperienceType levelOfExperienceType)
        {
            using(FisharooDataContext dc = conn.GetContext())
            {
                dc.LevelOfExperienceTypes.Attach(levelOfExperienceType, true);
                dc.LevelOfExperienceTypes.DeleteOnSubmit(levelOfExperienceType);
                dc.SubmitChanges();
            }
        }
    }
}
