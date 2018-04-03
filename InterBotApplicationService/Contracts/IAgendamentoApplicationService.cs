using InterBotDomain.DomainEntities;

namespace InterBotApplicationService.ApplicationServices
{
    public interface IAgendamentoApplicationService
    {
        void CreateAgendamento(Agendamento agendamento);
    }
}