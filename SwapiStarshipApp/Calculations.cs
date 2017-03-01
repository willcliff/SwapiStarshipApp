using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SwapiStarshipApp
{
    /// <summary>
    /// Gives the number of hours in a single Day, Week, Month, Year
    /// </summary>
    enum TimeInHours
    {
        HoursInDay = 24,
        HoursInWeek = 168,
        HoursInMonth = 730,
        HoursInYear = 8760,
    };

    /// <summary>
    /// Performs the relevant calculations on the <see cref="SwapiStarshipApp.Entities.Starship" />.
    /// </summary>
    public class Calculations
    {
        /// <summary>
        /// Checks if the following variables of the <see cref="SwapiStarshipApp.Entities.Starship" /> have known values.
        /// </summary>
        /// <param name="checkConsumeableTime">The starship's consumeables.</param>
        /// <param name="checkSpeed">The starship's speed.</param>
        /// <returns>
        /// Returns bool
        /// </returns>
        public bool KnownConsumeablesAndSpeed(string checkConsumeableTime, string checkSpeed)
        {
            if (checkConsumeableTime == "unknown" || checkSpeed == "unknown")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Identifies and calculates the consumables time in hours of the of <see cref="SwapiStarshipApp.Entities.Starship" /> if it has known values <see cref="SwapiStarshipApp.Calculations.CalculateStopsRequired(string, int, int)" />.
        /// </summary>
        /// <param name="consumeableTime">The value of <see cref="SwapiStarshipApp.Entities.Starship" /> consumeables given in either hours, days , weeks, months or years.</param>
        /// <returns>
        /// Returns the consumeables time in hours.
        /// </returns>
        public double ConsumeableTimeInHours(string consumeableTime)
        {
            string hour = "hours", day = "day", week = "week", month = "month", year = "year";
            string extractNumber = Regex.Match(consumeableTime, @"\d+").Value;
            double timeInHours = 0;

            if (consumeableTime.IndexOf(hour, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                timeInHours = Convert.ToInt32(extractNumber);
            }
            else if (consumeableTime.IndexOf(day, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int timeInDays = Convert.ToInt32(extractNumber);
                timeInHours = timeInDays * (int)TimeInHours.HoursInDay;
            }
            else if (consumeableTime.IndexOf(week, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int timeInWeeks = Convert.ToInt32(extractNumber);
                timeInHours = timeInWeeks * (int)TimeInHours.HoursInWeek;
            }
            else if (consumeableTime.IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int timeInMonths = Convert.ToInt32(extractNumber);
                timeInHours = timeInMonths * (int)TimeInHours.HoursInMonth;
            }
            else if (consumeableTime.IndexOf(year, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                int timeInYears = Convert.ToInt32(extractNumber);
                timeInHours = timeInYears * (int)TimeInHours.HoursInYear;
            }
            return timeInHours;
        }

        /// <summary>
        /// Calculates the stops required for a of <see cref="SwapiStarshipApp.Entities.Starship" /> to reach a given distance <see cref="SwapiStarshipApp.Program" />.
        /// </summary>
        /// <param name="consumeableTime">The starship's consumeables given in hours.</param>
        /// <param name="distance">The distance required to travel in MGLT.</param>
        /// <param name="speed">The speed of the starhip in MGLT/hr.</param>
        /// <returns>
        /// Returns the number of stops required.
        /// </returns>
        public int CalculateStopsRequired(string consumeableTime, int distance, int speed)
        {
            int stopsRequired;
            double timeTaken;
            double resupplyTime = ConsumeableTimeInHours(consumeableTime);

            timeTaken = distance / speed;
            stopsRequired = (int)(timeTaken / resupplyTime);
            return stopsRequired;
        }
    }
}
