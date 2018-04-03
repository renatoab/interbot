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
        ICitiesRepository citiesRepository;

        public List<Schools> ListSchools()
        {
            citiesRepository = new CitiesRepository();
            IList<Schools> listSchools = new List<Schools>();
            listSchools.Add(new Schools ("Internetional House - Premium", citiesRepository.getCitiesByName("Berlin")));
            listSchools.Add(new Schools("Berlin Institute of Technology - Normal", citiesRepository.getCitiesByName("Berlin")));
            listSchools.Add(new Schools ( "Evanz - Normal" , citiesRepository.getCitiesByName("Quebec")));
            listSchools.Add(new Schools("Montreal College - Normal", citiesRepository.getCitiesByName("Quebec")));
            listSchools.Add(new Schools ( "Thompsom - Budget" , citiesRepository.getCitiesByName("Dublin")));
            listSchools.Add(new Schools("DIT - Normal", citiesRepository.getCitiesByName("Dublin")));

            return listSchools.ToList();
        }

        public List<Schools> ListSchoolsByCity(Cities city)
        {
            return ListSchools().Where(s => s.City.Nome == city.Nome).ToList();
        }
    }
}
