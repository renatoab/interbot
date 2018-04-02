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
    public class HousesDomainService : IHousesDomainService
    {
        private readonly IHousesRepository housesRepository;

        public HousesDomainService(IHousesRepository housesRepository)
        {
            this.housesRepository = housesRepository;
        }

        public List<Houses> ListHouses()
        {
            return housesRepository.ListHouses();
        }
    }
}
