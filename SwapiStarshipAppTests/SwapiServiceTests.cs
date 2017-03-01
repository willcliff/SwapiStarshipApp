using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwapiStarshipApp;
using SwapiStarshipApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwapiStarshipApp.Tests
{
    [TestClass()]
    public class SwapiServiceTests
    {
        SwapiService swapiService = new SwapiService();

        [TestMethod()]
        public void GetStarshipsTest_BreakWhenAllStarshipsReceived()
        {
            List<Starship> allStarships = new List<Starship>();
            var starships = swapiService.GetAllStarships();

            while (allStarships.Count < starships.count)
            {
                foreach (Starship starship in starships.results)
                {
                    allStarships.Add(starship);
                }
                if (allStarships.Count == starships.count)
                {
                    break;
                }
                starships = swapiService.GetAllStarships(starships.nextPageNo);
            }
            Assert.AreEqual(allStarships.Count, starships.count);
        }

        [TestMethod()]
        public void GetStarshipsTest_NoExtraCallToGetAllStarships()
        {
            List<Starship> allStarships = new List<Starship>();
            var starships = swapiService.GetAllStarships();
            bool expected = false;
            bool extraCall = true;
            while (allStarships.Count < starships.count)
            {
                foreach (Starship starship in starships.results)
                {
                    allStarships.Add(starship);
                }
                if (allStarships.Count == starships.count)
                {
                    extraCall = false;
                    break;
                }
                starships = swapiService.GetAllStarships(starships.nextPageNo);
                extraCall = true;
            }
            Assert.AreEqual(expected, extraCall);
        }

        [TestMethod()]
        public void GetAllStarshipsTest_ReturnsExpectedType()
        {
            string defaultPageNumber = "1";
            EntityResults<Starship> result = swapiService.GetAllPaginated<Starship>("/starships/", defaultPageNumber);
            Assert.IsInstanceOfType(result, typeof(EntityResults<Starship>));
        }
    }
}