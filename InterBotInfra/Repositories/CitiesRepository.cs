using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotInfra.Repositories
{
    public class CitiesRepository : ICitiesRepository
    {
        public List<Cities> listarCities()
        {
            IList<Cities> listCities = new List<Cities>();
            listCities.Add(new Cities { Nome = "Berlin", Info = "Alemanha - Alemão" });
            listCities.Add(new Cities { Nome = "Quebec", Info = "Canadá - Francês" });
            listCities.Add(new Cities { Nome = "Dublin", Info = "Irlanda - Inglês" });

            return listCities.ToList();
        }

        public Cities getCitiesByName(string name)
        {
            return listarCities().Where(c => c.Nome == name).FirstOrDefault();
        }
    }
}
