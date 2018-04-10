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
    class Startup
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("596056551:AAHF_u5VlopPp_v91WVnl-ng5ilGL4UuA9Q");

        private static Container container;

        private static IBotController botController;

        static void Main(string[] args)
        {
            ConfigureServices();

            botController = container.GetInstance<IBotController>();

            bot.OnMessage += botController.StartBot;
            bot.StartReceiving();
            Console.WriteLine("Bot Started");
            Console.ReadKey();
        }

        private static void ConfigureServices()
        {
            container = new Container();

            container.Register<IBotController, BotController>(Lifestyle.Singleton);
            container.Register<IBotAdapter, BotAdapter>(Lifestyle.Singleton);
            container.RegisterInstance<TelegramBotClient>(bot);

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
    }
}
