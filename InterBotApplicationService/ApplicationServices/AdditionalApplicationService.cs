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
    public class AdditionalApplicationService : IAdditionalApplicationService
    {
        private readonly IAdditionalDomainService additionalDomainSercice;

        public AdditionalApplicationService(IAdditionalDomainService additionalDomainSercice)
        {
            this.additionalDomainSercice = additionalDomainSercice;
        }

        public List<Additional> ListAdditional()
        {
            return additionalDomainSercice.ListAdditional();
        }
    }
}
