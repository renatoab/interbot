using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainServices;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using InterBotInfra.Repositories;
using SimpleInjector.Lifestyles;
using InterBotDomain.Contracts.Services;
using InterBotApplicationService.Contracts;
using InterBotApplicationService.ApplicationServices;
using InterBotDomain.DomainEntities;
using System.Text.RegularExpressions;
using System.Globalization;

namespace InterBotConsoleApp
{
    class Program
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("453316303:AAGE7ymlyh7iMvPyMr9dq-zd2OkafwFdvkA");
        private static Dictionary<int, string> UserSteps = new Dictionary<int, string>();
        private static Container container;
        private static Agendamento agendamento;
        private static List<string> lista;

        static void Main(string[] args)
        {
            ConfigurarDependencias();

            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Console.WriteLine("Bot Started");
            Console.ReadKey();
        }

        private static void ConfigurarDependencias()
        {
            //configurando o simple injector
            container = new Container();

            container.Register<ICitiesApplicationService, CitiesApplicationService>(Lifestyle.Singleton);
            container.Register<IHousesApplicationService, HousesApplicationService>(Lifestyle.Singleton);
            container.Register<ISchoolsApplicationService, SchoolsApplicationService>(Lifestyle.Singleton);
            container.Register<IAdditionalApplicationService, AdditionalApplicationService>(Lifestyle.Singleton);
            container.Register<IAgendamentoApplicationService, AgendamentoApplicationService>(Lifestyle.Singleton);

            container.Register<ICitiesDomainService, CitiesDomainService>(Lifestyle.Singleton);
            container.Register<IHousesDomainService, HousesDomainService>(Lifestyle.Singleton);
            container.Register<ISchoolsDomainService, SchoolsDomainService>(Lifestyle.Singleton);
            container.Register<IAdditionalDomainService, AdditionalDomainService>(Lifestyle.Singleton);
            container.Register<IAgendamentoDomainService, AgendamentoDomainService>(Lifestyle.Singleton);

            container.Register<ICitiesRepository, CitiesRepository>(Lifestyle.Singleton);
            container.Register<IHousesRepository, HousesRepository>(Lifestyle.Singleton);
            container.Register<ISchoolsRepository, SchoolsRepository>(Lifestyle.Singleton);
            container.Register<IAdditionalRepository, AdditionalRepository>(Lifestyle.Singleton);
            container.Register<IAgendamentoRepository, AgendamentoRepository>(Lifestyle.Singleton);

            container.Verify();
        }

        private static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.TextMessage)
            {
                //Contato Inicial ou reiniciando o bot
                if (e.Message.Text.Contains("/start"))
                {
                    Send(e, "Olá " + e.Message.Chat.Username + "\n Escolha uma cidade");
                    var citiesApplicationService = container.GetInstance<CitiesApplicationService>();
                    lista = citiesApplicationService.ListCities().ConvertAll(c => $"{c.Nome} - {c.Info}");
                    Send(e, lista);
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
                else
                {
                    if (UserSteps.Count == 0)
                        return;

                    switch (UserSteps[e.Message.From.Id])
                    {

                        case "Cidade":


                            if (lista.Find(c => c.Contains(e.Message.Text)) != null)
                            {
                                agendamento.Cidade = e.Message.Text;

                                Send(e, e.Message.From.Username + "\n Escolha uma acomodação");
                                var housesApplicationService = container.GetInstance<HousesApplicationService>();
                                lista = housesApplicationService.ListHouses().ConvertAll(h => h.Nome);
                                Send(e, lista);
                                UserSteps[e.Message.From.Id] = "Acomodação";

                            }
                            else
                            {
                                Send(e, "Comando Inválido. Escolha uma das cidades");
                                Send(e, lista);
                            }

                            break;

                        case "Acomodação":


                            if (lista.Find(c => c.Contains(e.Message.Text)) != null)
                            {
                                agendamento.Acomodacao = e.Message.Text;

                                Send(e, e.Message.From.Username + "\n Escolha uma escola");
                                var schoolsApplicationService = container.GetInstance<SchoolsApplicationService>();
                                var citiesApplicationService = container.GetInstance<CitiesApplicationService>();
                                lista = schoolsApplicationService.ListSchoolsByCity(citiesApplicationService.getCitiesByName(agendamento.Cidade)).ConvertAll(s => s.Nome);
                                Send(e, lista);
                                UserSteps[e.Message.From.Id] = "Escola";

                            }
                            else
                            {
                                Send(e, "Comando Inválido. Escolha uma das acomodações");
                                Send(e, lista);
                            }

                            break;

                        case "Escola":


                            if (lista.Find(c => c.Contains(e.Message.Text)) != null)
                            {
                                agendamento.Escola = e.Message.Text;

                                Send(e, e.Message.From.Username + "\n Escolha uma dos adicionais");
                                var additionalApplicationService = container.GetInstance<AdditionalApplicationService>();
                                lista = additionalApplicationService.ListAdditional().ConvertAll(a => a.Nome);
                                Send(e, lista);
                                UserSteps[e.Message.From.Id] = "Adicional";

                            }
                            else
                            {
                                Send(e, "Comando Inválido. Escolha uma das escolas");
                                Send(e, lista);
                            }
                            break;

                        case "Adicional":


                            if (lista.Find(c => c.Contains(e.Message.Text)) != null)
                            {
                                agendamento.Adicionais = e.Message.Text;

                                Send(e, e.Message.From.Username + "\n Digite seu tempo de permanencia em dias, meses ou anos");
                                UserSteps[e.Message.From.Id] = "Tempo";

                            }
                            else
                            {
                                Send(e, "Comando Inválido. Escolha uma dos adicionais");
                                Send(e, lista);
                            }

                            break;

                        case "Tempo":


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
                                Send(e, lista);
                            }

                            break;

                        case "Confirmação":


                            if (e.Message.Text == "Sim")
                            {
                                Send(e, "Diga quando você virá a agencia");
                                UserSteps[e.Message.From.Id] = "Agendamento";
                                                                
                            }
                            else if (e.Message.Text == "Não")
                            {
                                agendamento.Status = AgendamentoStatus.Iniciado;
                                var agendamentoApplicationService = container.GetInstance<AgendamentoApplicationService>();
                                agendamentoApplicationService.CreateAgendamento(agendamento);

                                agendamento = new Agendamento();
                                agendamento.Nome = e.Message.From.Username;

                                Send(e, "Reiniciando...Escolha a cidade");
                                var citiesApplicationService = container.GetInstance<CitiesApplicationService>();
                                lista = citiesApplicationService.ListCities().ConvertAll(c => $"{c.Nome} - {c.Info}");
                                Send(e, lista);
                                UserSteps[e.Message.From.Id] = "Cidade";
                            }
                            else
                            {
                                Send(e, "Comando Inválido. Confirmar Solicitação ?(Sim/Não)");
                            }

                            break;

                        case "Agendamento":

                            if (IsValidDateTimeTest(e.Message.Text))
                            {
                                Send(e, "Visita Agendada!");
                                UserSteps[e.Message.From.Id] = "Finalizado";
                                agendamento.VisitaNaAgencia = e.Message.Text;

                                agendamento.Status = AgendamentoStatus.Concluido;
                                var agendamentoApplicationService = container.GetInstance<AgendamentoApplicationService>();
                                agendamentoApplicationService.CreateAgendamento(agendamento);

                            }
                            else
                            {
                                Send(e, "Data Invalida");
                            }

                            break;

                        default:
                            break;

                    }
                }
            }
        }

        public static void Send(Telegram.Bot.Args.MessageEventArgs e, string msg)
        {
            bot.SendTextMessageAsync(e.Message.Chat.Id, msg);
        }

        public static void Send(Telegram.Bot.Args.MessageEventArgs e, List<string> msgList)
        {
            var msg = msgList.Aggregate((i, j) => i + "\n" + j);
            Send(e, msg);
        }

        public static bool IsValidTime(string time)
        {
            var regex = new Regex("[0-9]* [a-z|A-Z]*");
            return regex.Match(time).Success;

        }

        public static bool IsValidDateTimeTest(string dateTime)
        {
            string[] formats = { "dd/MM/yyyy" };
            DateTime parsedDateTime;
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("pt-BR"),
                                           DateTimeStyles.None, out parsedDateTime);
        }

    }
}
