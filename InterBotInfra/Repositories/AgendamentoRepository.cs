using InterBotDomain.DomainEntities;
using InterBotDomain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterBotInfra.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private List<Agendamento> Agendamentos;

        public void createAgendamento(Agendamento agendamento)
        {
            if (Agendamentos == null)
                Agendamentos = new List<Agendamento>();
            Agendamentos.Add(agendamento);
        }
    }
}
