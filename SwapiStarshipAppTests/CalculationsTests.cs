using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwapiStarshipApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SwapiStarshipApp.Tests
{

    enum TimeInHours
    {
        HoursInDay = 24,
        HoursInWeek = 168,
        HoursInMonth = 730,
        HoursInYear = 8760,
    };

    [TestClass()]
    public class CalculationsTests
    {
        [TestMethod()]
        public void KnownConsumeablesAndSpeedTest_BothUnknown()
        {
            string checkConsumeableTime = "unknown";
            string checkSpeed = "unknown";
            bool knownSpeedAndConsumeable = true;
            bool expected = false;
            if (checkConsumeableTime == "unknown" || checkSpeed == "unknown")
            {
                knownSpeedAndConsumeable = false;
            }
            Assert.AreEqual(expected, knownSpeedAndConsumeable);
        }

        [TestMethod()]
        public void KnownConsumeablesAndSpeedTest_SingleUnknown()
        {
            string checkConsumeableTime = "1 month";
            string checkSpeed = "unknown";
            bool knownSpeedAndConsumeable = true;
            bool expected = false;
            if (checkConsumeableTime == "unknown" || checkSpeed == "unknown")
            {
                knownSpeedAndConsumeable = false;
            }
            Assert.AreEqual(expected, knownSpeedAndConsumeable);
        }

        [TestMethod()]
        public void KnownConsumeablesAndSpeedTest_BothKnown()
        {
            string checkConsumeableTime = "1 month";
            string checkSpeed = "70";
            bool knownSpeedAndConsumeable = true;
            bool expected = true;
            if (checkConsumeableTime == "unknown" || checkSpeed == "unknown")
            {
                knownSpeedAndConsumeable = false;
            }
            Assert.AreEqual(expected, knownSpeedAndConsumeable);
        }

        [TestMethod()]
        public void ConsumeableTimeInHoursTest_ExtractNumber()
        {
            string consumeableTime = "2 Weeks";
            string expected = "2";
            string extractNumber = Regex.Match(consumeableTime, @"\d+").Value;

            Assert.AreEqual(expected, extractNumber);
        }

        [TestMethod()]
        public void ConsumeableTimeInHoursTest_IdentifyTimePeriodSHouldPass()
        {
            string consumeableTime = "2 Weeks";
            string hour = "hours", day = "day", week = "week", month = "month", year = "year";
            string identifiedTimePeriod = null;
            string expected = "week";
            if (consumeableTime.IndexOf(hour, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                identifiedTimePeriod = "hour";
            }
            else if (consumeableTime.IndexOf(day, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                identifiedTimePeriod = "day";
            }
            else if (consumeableTime.IndexOf(week, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                identifiedTimePeriod = "week";
            }
            else if (consumeableTime.IndexOf(month, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                identifiedTimePeriod = "month";
            }
            else if (consumeableTime.IndexOf(year, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                identifiedTimePeriod = "year";
            }

            Assert.AreEqual(expected, identifiedTimePeriod);
        }

        [TestMethod()]
        public void CalculateStopsRequiredTest_TimeTaken()
        {
            int distance = 1000000;
            int speed = 80;
            double timeTaken;
            double expected = 12500;

            timeTaken = distance / speed;
            Assert.AreEqual(expected, timeTaken);
        }

        [TestMethod()]
        public void CalculateStopsRequiredTest_StopsRequired()
        {
            int stopsRequired;
            int consumeableTimeInWeeks = 1;
            double resupplyTime = consumeableTimeInWeeks * (int)TimeInHours.HoursInWeek; //=336
            double timeTaken = 12500;
            double expected = 74;

            stopsRequired = (int)(timeTaken / resupplyTime);
            Assert.AreEqual(expected, stopsRequired);
        }
    }
}