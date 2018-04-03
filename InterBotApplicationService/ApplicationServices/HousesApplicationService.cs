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
    public class HousesApplicationService : IHousesApplicationService
    {
        private readonly IHousesDomainService housesDomainService;

        public HousesApplicationService(IHousesDomainService housesDomainService)
        {
            this.housesDomainService = housesDomainService;
        }

        public List<Houses> ListHouses()
        {
            return housesDomainService.ListHouses();
        }
    }
}
