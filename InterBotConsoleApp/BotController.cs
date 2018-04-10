using InterBotApplicationService.ApplicationServices;
using InterBotApplicationService.Contracts;
using InterBotDomain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;

namespace InterBotConsoleApp
{
    public class BotController : IBotController 
    {
        private List<string> list;
        private Dictionary<int, string> UserSteps = new Dictionary<int, string>();
        private Agendamento agendamento;
        
        private readonly ICitiesApplicationService citiesApplicationService;
        private readonly IHousesApplicationService housesApplicationService;
        private readonly ISchoolsApplicationService schoolsApplicationService;
        private readonly IAdditionalApplicationService additionalApplicationService;
        private readonly IAgendamentoApplicationService agendamentoApplicationService;
        private readonly IBotAdapter botAdapter;

        public BotController(ICitiesApplicationService citiesApplicationService,
                             IHousesApplicationService housesApplicationService,
                             ISchoolsApplicationService schoolsApplicationService,
                             IAdditionalApplicationService additionalApplicationService,
                             IAgendamentoApplicationService agendamentoApplicationService,
                             IBotAdapter botAdapter)
        {
            this.citiesApplicationService = citiesApplicationService;
            this.housesApplicationService = housesApplicationService;
            this.schoolsApplicationService = schoolsApplicationService;
            this.additionalApplicationService = additionalApplicationService;
            this.agendamentoApplicationService = agendamentoApplicationService;
            this.botAdapter = botAdapter;
        }


        public void StartBot(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                
                if (e.Message.Text.Contains("/start"))
                {
                    IniciarAtendimento(e);
                }
                else
                {
                    if (UserSteps.Count == 0)
                        return;

                    switch (UserSteps[e.Message.From.Id])
                    {

                        case "Cidade":

                            ValidarCidade(e);
                            break;

                        case "Acomodação":

                            ValidarAcomodacao(e);
                            break;

                        case "Escola":

                            ValidarEscola(e);
                            break;

                        case "Adicional":

                            ValidarAdicional(e);
                            break;

                        case "Tempo":

                            ValidarTempoDePermanencia(e);
                            break;

                        case "Confirmação":

                            ValidarConfirmacao(e);
                            break;

                        case "Agendamento":

                            ValidarAgendamento(e);
                            break;

                        default:
                            break;

                    }
                }
            }
        }

        private void ValidarAgendamento(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (IsValidDateTimeTest(e.Message.Text))
            {
                Send(e, "Visita Agendada!");
                UserSteps[e.Message.From.Id] = "Finalizado";
                agendamento.VisitaNaAgencia = e.Message.Text;

                agendamento.Status = AgendamentoStatus.Concluido;
                agendamentoApplicationService.CreateAgendamento(agendamento);

            }
            else
            {
                Send(e, "Data Invalida");
            }
        }

        private void ValidarConfirmacao(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Text == "Sim")
            {
                Send(e, "Diga quando você virá a agencia");
                UserSteps[e.Message.From.Id] = "Agendamento";

            }
            else if (e.Message.Text == "Não")
            {
                agendamento.Status = AgendamentoStatus.Iniciado;
                agendamentoApplicationService.CreateAgendamento(agendamento);

                agendamento = new Agendamento();
                agendamento.Nome = e.Message.From.Username;

                Send(e, "Reiniciando...Escolha a cidade");
                list = citiesApplicationService.ListCities().ConvertAll(c => $"{c.Nome} - {c.Info}");
                Send(e, list);
                UserSteps[e.Message.From.Id] = "Cidade";
            }
            else
            {
                Send(e, "Comando Inválido. Confirmar Solicitação ?(Sim/Não)");
            }
        }
        
        private void ValidarTempoDePermanencia(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (IsValidTime(e.Message.Text))
            {
                agendamento.Tempo = e.Message.Text;

                Send(e, "Solicitação de Intercambio\n-Cidade: " + agendamento.Cidade + "\n-Acomodação: " + agendamento.Acomodacao + "\n-Escola: " + agendamento.Escola + "\n-Adicional: " + agendamento.Adicionais + "\n-Tempo de Permanencia: " + agendamento.Tempo);
                Send(e, e.Message.From.Username + "\n Confirmar Solicitação ?(Sim/Não)");
                UserSteps[e.Message.From.Id] = "Confirmação";

            }
            else
            {
                Send(e, "Comando Inválido. Digite seu tempo de permanencia em dias, meses ou anos");
                Send(e, list);
            }
        }

        private void ValidarAdicional(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (list.Find(c => c.Contains(e.Message.Text)) != null)
            {
                agendamento.Adicionais = e.Message.Text;

                Send(e, e.Message.From.Username + "\n Digite seu tempo de permanencia em dias, meses ou anos");
                UserSteps[e.Message.From.Id] = "Tempo";

            }
            else
            {
                Send(e, "Comando Inválido. Escolha uma dos adicionais");
                Send(e, list);
            }
        }

        private void ValidarEscola(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (list.Find(c => c.Contains(e.Message.Text)) != null)
            {
                agendamento.Escola = e.Message.Text;

                Send(e, e.Message.From.Username + "\n Escolha uma dos adicionais");
                list = additionalApplicationService.ListAdditional().ConvertAll(a => a.Nome);
                Send(e, list);
                UserSteps[e.Message.From.Id] = "Adicional";

            }
            else
            {
                Send(e, "Comando Inválido. Escolha uma das escolas");
                Send(e, list);
            }
        }

        private void ValidarAcomodacao(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (list.Find(c => c.Contains(e.Message.Text)) != null)
            {
                agendamento.Acomodacao = e.Message.Text;

                Send(e, e.Message.From.Username + "\n Escolha uma escola");
                list = schoolsApplicationService.ListSchoolsByCity(citiesApplicationService.getCitiesByName(agendamento.Cidade)).ConvertAll(s => s.Nome);
                Send(e, list);
                UserSteps[e.Message.From.Id] = "Escola";

            }
            else
            {
                Send(e, "Comando Inválido. Escolha uma das acomodações");
                Send(e, list);
            }
        }

        private void ValidarCidade(Telegram.Bot.Args.MessageEventArgs e)
        {
            if (list.Find(c => c.Contains(e.Message.Text)) != null)
            {
                agendamento.Cidade = e.Message.Text;

                Send(e, e.Message.From.Username + "\n Escolha uma acomodação");
                list = housesApplicationService.ListHouses().ConvertAll(h => h.Nome);
                Send(e, list);
                UserSteps[e.Message.From.Id] = "Acomodação";

            }
            else
            {
                Send(e, "Comando Inválido. Escolha uma das cidades");
                Send(e, list);
            }
        }

        private void IniciarAtendimento(Telegram.Bot.Args.MessageEventArgs e)
        {
            Send(e, "Olá " + e.Message.Chat.Username + "\n Escolha uma cidade");
            list = citiesApplicationService.ListCities().ConvertAll(c => $"{c.Nome} - {c.Info}");
            Send(e, list);
            if (!UserSteps.ContainsKey(e.Message.From.Id))
            {
                UserSteps.Add(e.Message.From.Id, "Cidade");
            }
            else
            {
                UserSteps[e.Message.From.Id] = "Cidade";
            }

            agendamento = new Agendamento();
            agendamento.Nome = e.Message.From.FirstName;
        }

        public void Send(Telegram.Bot.Args.MessageEventArgs e, string msg)
        {
            botAdapter.EnviarMensagem(e.Message.Chat.Id, msg);
        }

        public  void Send(Telegram.Bot.Args.MessageEventArgs e, List<string> msgList)
        {
            var msg = msgList.Aggregate((i, j) => i + "\n" + j);
            Send(e, msg);
        }

        public bool IsValidTime(string time)
        {
            var regex = new Regex("[0-9]* [a-z|A-Z]*");
            return regex.Match(time).Success;

        }

        public bool IsValidDateTimeTest(string dateTime)
        {
            string[] formats = { "dd/MM/yyyy" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("pt-BR"),
                                           DateTimeStyles.None, out parsedDateTime);
        }

    }
}
