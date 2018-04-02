using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotInfra.Repositories
{
    public class AdditionalRepository : IAdditionalRepository
    {
        public List<Additional> ListAdditional()
        {
            IList<Additional> listAdditional = new List<Additional>();
            listAdditional.Add(new Additional { Nome = "Transfer" });
            listAdditional.Add(new Additional { Nome = "Workshop" });
            listAdditional.Add(new Additional { Nome = "Nenhum" });

            return listAdditional.ToList();
        }
    }
}
