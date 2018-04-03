using InterBotApplicationService.Contracts;
using InterBotDomain.Contracts.Services;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotApplicationService.ApplicationServices
{
    public class CitiesApplicationService : ICitiesApplicationService
    {
        private readonly ICitiesDomainService citiesDomainService;

        public CitiesApplicationService(ICitiesDomainService citiesDomainService)
        {
            this.citiesDomainService = citiesDomainService;
        }

        public List<Cities> ListCities()
        {
            return citiesDomainService.ListCities();
        }
    }
}
