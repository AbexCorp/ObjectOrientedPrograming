using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TimeTimePeriod;
using TimeTimePeriod.Structures;

namespace UnitTests
{
    [TestClass]
    public class TimeStructUnitTests
    {
        private void AssertTime(Time t, byte expectedHours, byte expectedMinutes, byte expectedSeconds) 
        {
            Assert.AreEqual(t.Hours, expectedHours);
            Assert.AreEqual(t.Minutes, expectedMinutes);
            Assert.AreEqual(t.Seconds, expectedSeconds);
        }

        #region Constructors ====================================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Time t = new Time();
            AssertTime(t, 0, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0,       (byte)0, (byte)0, (byte)0)]
        [DataRow(23, 59, 59,    (byte)23, (byte)59, (byte)59)]
        [DataRow(7, 23, 5,      (byte)7, (byte)23, (byte)5)]
        [DataRow(15, 9, 41,     (byte)15, (byte)9, (byte)41)]
        public void Constructor_3Parameters(int hours, int minutes, int seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours, minutes, seconds);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0,      (byte)0, (byte)0, (byte)0)]
        [DataRow(23, 59,    (byte)23, (byte)59, (byte)0)]
        [DataRow(7, 23,     (byte)7, (byte)23, (byte)0)]
        [DataRow(15, 9,     (byte)15, (byte)9, (byte)0)]
        public void Constructor_2Parameters(int hours, int minutes, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours, minutes);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0,     (byte)0, (byte)0, (byte)0)]
        [DataRow(23,    (byte)23, (byte)0, (byte)0)]
        [DataRow(7,     (byte)7, (byte)0, (byte)0)]
        [DataRow(15,    (byte)15, (byte)0, (byte)0)]
        public void Constructor_1Parameter(int hours, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, -1)]
        [DataRow(0, -1, 0)]
        [DataRow(0, -1, -1)]
        [DataRow(-1, 0, 0)]
        [DataRow(-1, 0, -1)]
        [DataRow(-1, -1, 0)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 0, 60)]
        [DataRow(0, 60, 0)]
        [DataRow(0, 60, 60)]
        [DataRow(24, 0, 0)]
        [DataRow(24, 0, 60)]
        [DataRow(24, 60, 0)]
        [DataRow(24, 60, 60)]
        [DataRow(24, -1, 60)]
        [DataRow(24, 60, -1)]
        [DataRow(-1, 60, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3Parameters_ArgumentOutOfRangeException(int hours, int minutes, int seconds)
        {
            Time t = new Time(hours, minutes, seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, -1)]
        [DataRow(-1, 0)]
        [DataRow(-1, -1)]
        [DataRow(0, 60)]
        [DataRow(24, 0)]
        [DataRow(24, 60)]
        [DataRow(24, -1)]
        [DataRow(-1, 60)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2Parameters_ArgumentOutOfRangeException(int hours, int minutes)
        {
            Time t = new Time(hours, minutes);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(24)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1Parameter_ArgumentOutOfRangeException(int hours)
        {
            Time t = new Time(hours);
        }
        #endregion
    }
}