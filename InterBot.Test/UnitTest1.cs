using System;
using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainServices;
using InterBotInfra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterBot.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestListCities()
        {
            ICitiesRepository citiesRepository = new CitiesRepository();

            CitiesDomainService citiesDomainService = new CitiesDomainService(citiesRepository);

            var lista = citiesDomainService.listarCities();

            Assert.IsTrue(lista.Count > 0);
        }
    }
}
