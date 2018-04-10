using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using InterBotApplicationService.Contracts;
using InterBotApplicationService.ApplicationServices;
using InterBotConsoleApp;
using SimpleInjector;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Args;

namespace InterBotTest
{
    [TestClass]
    public class InterBotTest
    {
        [TestMethod]
        public void Test_Flow()
        {
            //Arrange
            Container container = new Container();
            
            Mock<ICitiesApplicationService> citiesApplicationService = new Mock<ICitiesApplicationService>();
            Mock<IHousesApplicationService> housesApplicationService = new Mock<IHousesApplicationService>();
            Mock<ISchoolsApplicationService> schoolsApplicationService = new Mock<ISchoolsApplicationService>();
            Mock<IAdditionalApplicationService> additionalApplicationService = new Mock<IAdditionalApplicationService>();
            Mock<IAgendamentoApplicationService> agendamentoApplicationService = new Mock<IAgendamentoApplicationService>();
            Mock<IBotAdapter> botAdapter = new Mock<IBotAdapter>();
           
            container.Register<IBotController, BotController>(Lifestyle.Singleton);
            container.RegisterInstance(citiesApplicationService.Object);
            container.RegisterInstance(housesApplicationService.Object);
            container.RegisterInstance(schoolsApplicationService.Object);
            container.RegisterInstance(additionalApplicationService.Object);
            container.RegisterInstance(agendamentoApplicationService.Object);
            container.RegisterInstance(botAdapter.Object);

            BotController botController = container.GetInstance<BotController>();          

        }
    }
}
        
    

