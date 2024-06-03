using System.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.DataAccess.Impl
{
    [Pluggable("Default")]
    public class PersonRepository : IPersonRepository
    {
        public List<string> GetAllNames()
        {
            List<string> names = new List<string>();

            FisharooDataContext dc = Connection.GetContext();

            var persons = from p in dc.Persons
                          select p;

            foreach (Person p in persons)
            {
                names.Add(p.FirstName + " " + p.LastName);
            }

            return names;
        }
    }
}
