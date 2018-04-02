using System;
using InterBotDomain.Contracts.Repositories;
using InterBotDomain.DomainServices;
using InterBotInfra.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterBotTest
{
    [TestClass]
    public class InterBotTest
    {
        [TestMethod]
        public void TestListCities()
        {
            CitiesRepository citiesRepository = new CitiesRepository();
            var listCities = citiesRepository.listarCities();

            Assert.IsTrue(listCities.Count > 0);

        }
    }
}
