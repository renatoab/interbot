using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotDomain.DomainEntities
{
    public class Schools
    {
        public Schools(string name, Cities city)
        {
            Nome = name;
            City = city;
        }

        public string Nome { get; set; }
        public virtual Cities City { get; set; }
    }
}
