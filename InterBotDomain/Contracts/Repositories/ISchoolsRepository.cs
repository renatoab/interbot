using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotDomain.Contracts.Repositories
{
    public interface ISchoolsRepository
    {
        List<Schools> ListSchools();
    }
}
