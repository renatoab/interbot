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
    public class SchoolsDomainService : ISchoolsDomainService
    {
        private readonly ISchoolsRepository schoolsRepository;

        public SchoolsDomainService(ISchoolsRepository schoolsRepository)
        {
            this.schoolsRepository = schoolsRepository;
        }

        public List<Schools> ListSchools()
        {
            return schoolsRepository.ListSchools();
        }
    }
}
