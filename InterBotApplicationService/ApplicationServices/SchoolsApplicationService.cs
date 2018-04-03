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
    public class SchoolsApplicationService : ISchoolsApplicationService
    {
        private readonly ISchoolsDomainService schoolsDomainService;

        public SchoolsApplicationService(ISchoolsDomainService schoolsDomainService)
        {
            this.schoolsDomainService = schoolsDomainService;
        }

        public List<Schools> ListSchools()
        {
            return schoolsDomainService.ListSchools();
        }
    }
}
