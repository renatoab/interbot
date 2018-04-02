using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotInfra.Repositories
{
    public class HousesRepository : IHousesRepository
    {
        public List<Houses> ListHouses()
        {
            IList<Houses> listHouses = new List<Houses>();
            listHouses.Add(new Houses { Nome = "Casa de Familia" });
            listHouses.Add(new Houses { Nome = "Hostel" });
            listHouses.Add(new Houses { Nome = "Flat" });

            return listHouses.ToList();
        }
    }
}
