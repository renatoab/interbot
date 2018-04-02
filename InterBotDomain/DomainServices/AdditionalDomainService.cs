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
    public class AdditionalDomainService : IAdditionalDomainService
    {
        private readonly IAdditionalRepository additionalRepository;

        public AdditionalDomainService(IAdditionalRepository additionalRepository)
        {
            this.additionalRepository = additionalRepository;
        }

        public List<Additional> ListAdditional()
        {
            return additionalRepository.ListAdditional();
        }
    }
}
