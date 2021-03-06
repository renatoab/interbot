﻿using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotApplicationService.Contracts
{
    public interface ISchoolsApplicationService
    {
        List<Schools> ListSchools();
        List<Schools> ListSchoolsByCity(Cities city);
    }
}
