using InterBotDomain.Contracts.Repositories;
using InterBotDomain.Contracts.Services;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotDomain.DomainServices
{
    public class CitiesDomainService : ICitiesDomainService
    {
        private readonly ICitiesRepository citiesRepository;

        public CitiesDomainService(ICitiesRepository citiesRepository)
        {
            this.citiesRepository = citiesRepository;
        }

        public List<Cities> ListCities()
        {
            return citiesRepository.listarCities();
        }
        
    }
}
