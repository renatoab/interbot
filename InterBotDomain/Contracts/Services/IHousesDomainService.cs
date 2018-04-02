using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotDomain.Contracts.Services
{
    public interface IHousesDomainService
    {
        List<Houses> ListHouses();
    }
}
