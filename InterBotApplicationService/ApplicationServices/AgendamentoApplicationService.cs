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
    public class AgendamentoApplicationService : IAgendamentoApplicationService
    {
        private readonly IAgendamentoDomainService agendamentoDomainSercice;

        public AgendamentoApplicationService(IAgendamentoDomainService agendamentoDomainSercice)
        {
            this.agendamentoDomainSercice = agendamentoDomainSercice;
        }

        public void CreateAgendamento(Agendamento agendamento)
        {
            agendamentoDomainSercice.CreateAgendamento(agendamento);
        }
    }
}
