using System;
using System.Collections.Generic;
using System.Linq;
using SwapiStarshipApp.Entities;

namespace SwapiStarshipApp
{
    /// <summary>
    /// This program calculates and outputs the amount of stops required for each starship tp travel a given distance.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the distance required to travel in MGLT:");
            string input = Console.ReadLine();
            int distance = Convert.ToInt32(input);

            Console.WriteLine("The following data conveys the Starships along with the required stops needed to travel {0} MGLT:", string.Format("{0:n0}", distance));
            Console.WriteLine("'Unknown' is displayed for the Starships where the speed or the consumeables value was unknown");
            Console.WriteLine();

            List<Starship> allStarships = new List<Starship>();
            SwapiService swapiService = new SwapiService();
            allStarships = swapiService.GetStarships();

            foreach (Starship starship in allStarships)
            {
                string checkConsumeables = starship.consumables;
                string checkSpeed = starship.MGLT;
                string stopsRequired = "unknown";
                Calculations calcualtions = new Calculations();

                if (calcualtions.KnownConsumeablesAndSpeed(checkConsumeables, checkSpeed))
                {
                    string consumeablesTime = starship.consumables;
                    int speed = Convert.ToInt32(starship.MGLT);
                    stopsRequired = calcualtions.CalculateStopsRequired(consumeablesTime, distance, speed).ToString();
                }
                Console.WriteLine("{0} : {1}", starship.name, stopsRequired);
            }
        }
    }
}

