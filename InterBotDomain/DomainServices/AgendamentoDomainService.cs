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
    public class AgendamentoDomainService : IAgendamentoDomainService
    {
        private readonly IAgendamentoRepository agendamentoRepository;

        public AgendamentoDomainService(IAgendamentoRepository agendamentoRepository)
        {
            this.agendamentoRepository = agendamentoRepository;
        }

        public void CreateAgendamento(Agendamento agendamento)
        {
            agendamentoRepository.createAgendamento(agendamento);
        }

    }
}
