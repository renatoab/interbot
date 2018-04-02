using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotInfra.Repositories
{
    public class SchoolsRepository : ISchoolsRepository
    {
        public List<Schools> ListSchools()
        {
            IList<Schools> listSchools = new List<Schools>();
            listSchools.Add(new Schools { Nome = "Internetional House - Premium" });
            listSchools.Add(new Schools { Nome = "Evanz - Normal" });
            listSchools.Add(new Schools { Nome = "Thompsom - Budget" });

            return listSchools.ToList();
        }
    }
}
