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
        [DataRow(0, 0, 0, (byte)0, (byte)0, (byte)0)]
        [DataRow(23, 59, 59, (byte)23, (byte)59, (byte)59)]
        [DataRow(7, 23, 5, (byte)7, (byte)23, (byte)5)]
        [DataRow(15, 9, 41, (byte)15, (byte)9, (byte)41)]
        public void Constructor_3Parameters(int hours, int minutes, int seconds, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours, minutes, seconds);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, (byte)0, (byte)0, (byte)0)]
        [DataRow(23, 59, (byte)23, (byte)59, (byte)0)]
        [DataRow(7, 23, (byte)7, (byte)23, (byte)0)]
        [DataRow(15, 9, (byte)15, (byte)9, (byte)0)]
        public void Constructor_2Parameters(int hours, int minutes, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(hours, minutes);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, (byte)0, (byte)0, (byte)0)]
        [DataRow(23, (byte)23, (byte)0, (byte)0)]
        [DataRow(7, (byte)7, (byte)0, (byte)0)]
        [DataRow(15, (byte)15, (byte)0, (byte)0)]
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

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1900, 7, 16, 16, 25, 38)]
        [DataRow(2023, 5, 6, 21, 19, 30)]
        public void Constructor_DateTime(int year, int month, int day, int hour, int minute, int second)
        {
            DateTime dt = new DateTime(year, month, day, hour, minute, second);
            Time t = new Time(dt);
            AssertTime(t, (byte)dt.Hour, (byte)dt.Minute, (byte)dt.Second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1900, 7, 16, 16, 25, 38)]
        [DataRow(2023, 5, 6, 21, 19, 30)]
        public void Constructor_DateTimeOffset(int year, int month, int day, int hour, int minute, int second)
        {
            DateTimeOffset dto = new DateTimeOffset(new DateTime(year, month, day, hour, minute, second));
            Time t = new Time(dto);
            AssertTime(t, (byte)dto.Hour, (byte)dto.Minute, (byte)dto.Second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0:0",         (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:0",        (byte)0,  (byte)0,  (byte)0)]
        [DataRow("0:00",        (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:00",       (byte)0,  (byte)0,  (byte)0)]
        [DataRow("0:0:0",       (byte)0,  (byte)0,  (byte)0)]
        [DataRow("0:0:00",      (byte)0,  (byte)0,  (byte)0)]
        [DataRow("0:00:0",      (byte)0,  (byte)0,  (byte)0)]
        [DataRow("0:00:00",     (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:0:0",      (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:0:00",     (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:00:0",     (byte)0,  (byte)0,  (byte)0)]
        [DataRow("00:00:00",    (byte)0,  (byte)0,  (byte)0)]
        [DataRow("23:59:59",    (byte)23, (byte)59, (byte)59)]
        [DataRow("23:59",       (byte)23, (byte)59, (byte)0)]
        [DataRow("7:23:5",      (byte)7,  (byte)23, (byte)5)]
        [DataRow("7:23:05",     (byte)7,  (byte)23, (byte)5)]
        [DataRow("07:23:5",     (byte)7,  (byte)23, (byte)5)]
        [DataRow("07:23:05",    (byte)7,  (byte)23, (byte)5)]
        [DataRow("7:23",        (byte)7,  (byte)23, (byte)0)]
        [DataRow("15:9:41",     (byte)15, (byte)9,  (byte)41)]
        [DataRow("15:09:41",    (byte)15, (byte)9,  (byte)41)]
        [DataRow("15:9",        (byte)15, (byte)9,  (byte)0)]
        [DataRow("15:09",       (byte)15, (byte)9,  (byte)0)]
        public void Constructor_String(string text, byte expectedHours, byte expectedMinutes, byte expectedSeconds)
        {
            Time t = new Time(text);
            AssertTime(t, expectedHours, expectedMinutes, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(" ")]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_String_ArgumentNullException(string text)
        {
            Time t = new Time(text);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("a")]
        [DataRow("a:a")]
        [DataRow("a:a:a")]
        [DataRow("a:a:a:a")]
        [DataRow("1:2:3:4")]
        [DataRow("12:")]
        [DataRow(":12")]
        [DataRow("12")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_String_FormatException(string text)
        {
            Time t = new Time(text);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("1:1:-1")]
        [DataRow("1:-1:1")]
        [DataRow("1:-1:-1")]
        [DataRow("-1:1:1")]
        [DataRow("-1:1:-1")]
        [DataRow("-1:-1:1")]
        [DataRow("-1:-1:-1")]
        [DataRow("0:0:60")]
        [DataRow("0:60:0")]
        [DataRow("0:60:60")]
        [DataRow("24:0:0")]
        [DataRow("24:0:60")]
        [DataRow("24:60:0")]
        [DataRow("24:60:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_String_ArgumentOutOfRangeException(string text)
        {
            Time t = new Time(text);
        }

        #endregion


        #region ToString ====================================================

        [DataTestMethod, TestCategory("ToString")]
        [DataRow("00:00:00", 0, 0, 0)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("23:59:00", 23, 59, 0)]
        [DataRow("07:23:05", 7, 23, 5)]
        [DataRow("14:03:34", 14, 3, 34)]
        [DataRow("03:23:00", 3, 23, 0)]
        [DataRow("15:09:41", 15, 9, 41)]
        public void ToString(string text, int Hours, int Minutes, int Seconds)
        {
            Time t = new Time(Hours, Minutes, Seconds);
            Assert.AreEqual(text, t.ToString());
        }

        [DataTestMethod, TestCategory("ToString")]
        [DataRow(0,0,0)]
        [DataRow(23,59,59)]
        [DataRow(23,59,0)]
        [DataRow(7,23,5)]
        [DataRow(14,3,34)]
        [DataRow(3,23,0)]
        [DataRow(15,9,41)]
        public void ToString_Pass_String_Constructor(int Hours, int Minutes, int Seconds)
        {
            Time t1 = new Time(Hours, Minutes, Seconds);
            Time t2 = new Time(t1.ToString());
            AssertTime(t2, (byte)Hours, (byte)Minutes, (byte)Seconds);
        }

        #endregion


        #region Equatable ====================================================

        [TestMethod, TestCategory("Equatable")]
        public void Equal_SameReference()
        {
            Time t1 = new Time(16, 5, 17);
            Assert.IsTrue(t1.Equals(t1));
        }

        [DataTestMethod, TestCategory("Equatable")]
        [DataRow(0,0,0)]
        [DataRow(1,5,7)]
        [DataRow(18,26,14)]
        [DataRow(11,39,51)]
        [DataRow(6,42,21)]
        public void Equal_IsEqual(int hour, int minute, int second) 
        {
            Time t1 = new Time(hour, minute, second);
            Time t2 = new Time(hour, minute, second);
            Assert.IsTrue(t1.Equals(t2));
            Assert.IsTrue(t2.Equals(t1));
        }

        [DataTestMethod, TestCategory("Equatable")]
        [DataRow(0,0,0, 0,0,1)]
        [DataRow(0,0,0, 0,1,0)]
        [DataRow(0,0,0, 0,1,1)]
        [DataRow(0,0,0, 1,0,0)]
        [DataRow(0,0,0, 1,0,1)]
        [DataRow(0,0,0, 1,1,0)]
        [DataRow(0,0,0, 1,1,1)]
        [DataRow(18,34,12, 14,17,5)]
        [DataRow(6,14,35,  8,46,7)]
        [DataRow(21,57,27, 15,29,13)]
        public void Equal_IsNotEqual(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsFalse(t1.Equals(t2));
            Assert.IsFalse(t2.Equals(t1));
        }

        #endregion


        #region Comparable ====================================================

        [TestMethod, TestCategory("Comparable")]
        public void CompareTo_SameReference()
        {
            Time t1 = new Time(16, 5, 17);
            Assert.AreEqual(t1.CompareTo(t1), 0);
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0, 0, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 0)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(1, 1, 1)]
        public void CompareTo_IsEqual(int hour, int minute, int second)
        {
            Time t1 = new Time(hour, minute, second);
            Time t2 = new Time(hour, minute, second);
            Assert.IsTrue( t1.CompareTo(t2) == 0 );
            Assert.IsTrue( t2.CompareTo(t1) == 0 );
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void CompareTo_IsBigger(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsTrue( t2.CompareTo(t1) > 0 );
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void CompareTo_IsSmaller(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1.CompareTo(t2) < 0);
        }

        #endregion


        #region Operators ====================================================

        [TestMethod, TestCategory("Operators")]
        public void Operator_Equal_NotEqual_SameReference()
        {
            Time t1 = new Time(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(t1 == t1);
            Assert.IsFalse(t1 != t1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 0)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(1, 1, 1)]
        public void Operator_Equal_NotEqual_IsEqual(int hour, int minute, int second)
        {
            Time t1 = new Time(hour, minute, second);
            Time t2 = new Time(hour, minute, second);
            Assert.IsTrue( t1 == t2 );
            Assert.IsTrue( t2 == t1 );
            Assert.IsFalse( t1 != t2 );
            Assert.IsFalse( t2 != t1 );
        }
        
        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_Equal_NotEqual_IsNotEqual(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1 != t2);
            Assert.IsTrue(t2 != t1);
            Assert.IsFalse(t1 == t2);
            Assert.IsFalse(t2 == t1);
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThan_LessThan_SameReference()
        {
            Time t1 = new Time(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsFalse(t1 > t1);
            Assert.IsFalse(t1 < t1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_GreaterThan_LessThan(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1 < t2);
            Assert.IsFalse(t2 < t1);
            Assert.IsFalse(t1 > t2);
            Assert.IsTrue(t2 > t1);
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_SameReference()
        {
            Time t1 = new Time(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(t1 >= t1);
            Assert.IsTrue(t1 <= t1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_IsEqual()
        {
            Time t1 = new Time(16, 5, 17);
            Time t2 = new Time(16, 5, 17);
            Assert.IsTrue(t1 >= t2);
            Assert.IsTrue(t2 >= t1);
            Assert.IsTrue(t1 <= t2);
            Assert.IsTrue(t2 <= t1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_IsDifferent(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            Time t1 = new Time(h1, m1, s1);
            Time t2 = new Time(h2, m2, s2);
            Assert.IsTrue(t1 <= t2);
            Assert.IsFalse(t2 <= t1);
            Assert.IsFalse(t1 >= t2);
            Assert.IsTrue(t2 >= t1);
        }

        #endregion
    }

    [TestClass]
    public class TimePeriodStructUnitTests
    {
        private void AssertTimePeriod(TimePeriod tp, byte expectedHour, byte expectedMinute, byte expectedSecond)
        {
            Assert.AreEqual(tp.Hours, expectedHour);
            Assert.AreEqual(tp.Minutes, expectedMinute);
            Assert.AreEqual(tp.Seconds, expectedSecond);
        }
        private void AssertTimePeriod(TimePeriod tp, byte expectedHour, byte expectedMinute, byte expectedSecond, long expectedDurationInSeconds)
        {
            Assert.AreEqual(tp.Hours, expectedHour);
            Assert.AreEqual(tp.Minutes, expectedMinute);
            Assert.AreEqual(tp.Seconds, expectedSecond);
            Assert.AreEqual(tp.DurationInSeconds, expectedDurationInSeconds);
        }

        #region Constructors ====================================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            TimePeriod tp = new TimePeriod();
            AssertTimePeriod(tp, 0, 0, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0,0,0,      0,0,0,     0)]
        [DataRow(154,18,9,   154,18,9,  ((154 * 3600) + (18 * 60) + 9) )]
        [DataRow(14,54,38,   14,54,38,  ((14 * 3600) + (54 * 60) + 38) )]
        [DataRow(7,27,51,    7,27,51,   ((7*3600)+(27*60)+51) )]
        public void Constructor_3Parameters(int hours, int minutes, int seconds, int expectedH, int expectedM, int expectedS, long expectedDurationInS)
        {
            TimePeriod tp = new TimePeriod((byte)hours, (byte)minutes, (byte)seconds);
            AssertTimePeriod(tp, (byte)expectedH, (byte)expectedM, (byte)expectedS, expectedDurationInS);
        }
        
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0,0,      0,0,      0)]
        [DataRow(154,18,   154,18,   ((154 * 3600) + (18 * 60)) )]
        [DataRow(14,54,    14,54,    ((14 * 3600) + (54 * 60)) )]
        [DataRow(7,27,     7,27,     ((7 * 3600) + (27 * 60)) )]
        public void Constructor_2Parameters(int hours, int minutes, int expectedH, int expectedM, long expectedDurationInS)
        {
            TimePeriod tp = new TimePeriod((byte)hours, (byte)minutes);
            AssertTimePeriod(tp, (byte)expectedH, (byte)expectedM, 0, expectedDurationInS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0,0,0,      0)]
        [DataRow(154,18,9,   ((154 * 3600) + (18 * 60) + 9) )]
        [DataRow(14,54,38,   ((14 * 3600) + (54 * 60) + 38) )]
        [DataRow(7,27,51,    ((7*3600)+(27*60)+51) )]
        public void Constructor_1Parameter(int expectedH, int expectedM, int expectedS, long DurationInSeconds)
        {
            TimePeriod tp = new TimePeriod(DurationInSeconds);
            AssertTimePeriod(tp, (byte)expectedH, (byte)expectedM, (byte)expectedS, DurationInSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(0,0, 60)]
        [DataRow(0,60,0)]
        [DataRow(0,60,60)]
        [DataRow(15,48,69)]
        [DataRow(15,69,48)]
        public void Constructor_3Parameters_ArgumentOutOfRangeException(int hours, int minutes, int seconds)
        {
            TimePeriod tp = new TimePeriod((byte)hours, (byte)minutes, (byte)seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow((byte)0,  (byte)60)]
        [DataRow((byte)15, (byte)69)]
        public void Constructor_2Parameters_ArgumentOutOfRangeException(byte hours, byte minutes)
        {
            TimePeriod tp = new TimePeriod(hours, minutes);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow(-1)]
        public void Constructor_1Parameter_ArgumentOutOfRangeException(long seconds)
        {
            TimePeriod tp = new TimePeriod(seconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(00,00,00,     00,00,00,     00,00,00)]
        [DataRow(00,00,00,     00,00,01,     00,00,01)]
        [DataRow(00,00,00,     00,01,00,     00,01,00)]
        [DataRow(00,00,00,     00,01,01,     00,01,01)]
        [DataRow(00,00,00,     01,00,00,     01,00,00)]
        [DataRow(00,00,00,     01,00,01,     01,00,01)]
        [DataRow(00,00,00,     01,01,00,     01,01,00)]
        [DataRow(00,00,00,     01,01,01,     01,01,01)]
        [DataRow(12,15,03,     12,15,03,     00,00,00)]
        [DataRow(00,00,00,     23,59,59,     23,59,59)]
        [DataRow(00,00,00,     12,00,00,     12,00,00)]
        [DataRow(11,00,00,     23,00,00,     12,00,00)]
        public void Constructor_2TimeParameters_EarlierTime(int h1, int m1, int s1, int h2, int m2, int s2, int expectedH, int expectedM, int expectedS)
        {
            Time t1 = new Time((byte)h1, (byte)m1, (byte)s1);
            Time t2 = new Time((byte)h2, (byte)m2, (byte)s2);
            TimePeriod tp = new TimePeriod(t1, t2);
            AssertTimePeriod(tp, (byte)expectedH, (byte)expectedM, (byte)expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(00,00,01,     00,00,00,     23,59,59)]
        [DataRow(00,01,00,     00,00,00,     23,59,00)]
        [DataRow(00,01,01,     00,00,00,     23,58,59)]
        [DataRow(01,00,00,     00,00,00,     23,00,00)]
        [DataRow(01,00,01,     00,00,00,     22,59,59)]
        [DataRow(01,01,00,     00,00,00,     22,59,00)]
        [DataRow(01,01,01,     00,00,00,     22,58,59)]
        [DataRow(23,59,59,     00,00,00,     00,00,01)]
        public void Constructor_2TimeParameters_LaterTime(int h1, int m1, int s1, int h2, int m2, int s2, int expectedH, int expectedM, int expectedS)
        {
            Time t1 = new Time((byte)h1, (byte)m1, (byte)s1);
            Time t2 = new Time((byte)h2, (byte)m2, (byte)s2);
            TimePeriod tp = new TimePeriod(t1, t2);
            AssertTimePeriod(tp, (byte)expectedH, (byte)expectedM, (byte)expectedS);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("1:1",      01,01,00)]
        [DataRow("1:01",     01,01,00)]
        [DataRow("01:1",     01,01,00)]
        [DataRow("01:01",    01,01,00)]
        [DataRow("1:1:1",    01,01,01)]
        [DataRow("1:1:01",   01,01,01)]
        [DataRow("1:01:1",   01,01,01)]
        [DataRow("1:01:01",  01,01,01)]
        [DataRow("01:1:1",   01,01,01)]
        [DataRow("01:1:01",  01,01,01)]
        [DataRow("01:01:1",  01,01,01)]
        [DataRow("01:01:01", 01,01,01)]
        [DataRow("9:48",     09,48,00)]
        [DataRow("15:27:56", 15,27,56)]
        [DataRow("21:04:5",  21,04,05)]
        public void Constructor_String(string text, int expectedHour, int expectedMinute, int expectedSecond)
        {
            TimePeriod tp = new TimePeriod(text);
            AssertTimePeriod(tp, (byte)expectedHour, (byte)expectedMinute, (byte)expectedSecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("  ")]
        [DataRow(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_String_ArgumentNullException(string text)
        {
            TimePeriod tp = new TimePeriod(text);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("a")]
        [DataRow("a:a")]
        [DataRow("a:a:a")]
        [DataRow("a:a:a:a")]
        [DataRow("1:2:3:4")]
        [DataRow("12:")]
        [DataRow(":12")]
        [DataRow("12")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_String_FormatException(string text)
        {
            TimePeriod tp = new TimePeriod(text);
        }

        #endregion


        #region ToString ====================================================

        [DataTestMethod, TestCategory("ToString")]
        [DataRow("00:00:00",    0,0,0)]
        [DataRow("23:59:59",    23,59,59)]
        [DataRow("23:59:00",    23,59,0)]
        [DataRow("07:23:05",    7,23,5)]
        [DataRow("14:03:34",    14,3,34)]
        [DataRow("03:23:00",    3,23,0)]
        [DataRow("15:09:41",    15,9,41)]
        [DataRow("115:09:41",   115,9,41)]
        public void ToString(string text, int Hours, int Minutes, int Seconds)
        {
            TimePeriod tp = new TimePeriod((byte)Hours, (byte)Minutes, (byte)Seconds);
            Assert.AreEqual(text, tp.ToString());
        }
        
        [DataTestMethod, TestCategory("ToString")]
        [DataRow(0,0,0)]
        [DataRow(23,59,59)]
        [DataRow(23,59,0)]
        [DataRow(7,23,5)]
        [DataRow(14,3,34)]
        [DataRow(3,23,0)]
        [DataRow(15,9,41)]
        [DataRow(115,9,41)]
        public void ToString_Pass_String_Constructor(int Hours, int Minutes, int Seconds)
        {
            TimePeriod tp1 = new TimePeriod((byte)Hours, (byte)Minutes, (byte)Seconds);
            TimePeriod tp2 = new TimePeriod(tp1.ToString());
            AssertTimePeriod(tp2, (byte)Hours, (byte)Minutes, (byte)Seconds);
        }

        #endregion


        #region Equatable ====================================================

        [TestMethod, TestCategory("Equatable")]
        public void Equal_SameReference()
        {
            TimePeriod tp1 = new TimePeriod(16, 5, 17);
            Assert.IsTrue(tp1.Equals(tp1));
        }

        [DataTestMethod, TestCategory("Equatable")]
        [DataRow(0, 0, 0)]
        [DataRow(1, 5, 7)]
        [DataRow(18, 26, 14)]
        [DataRow(11, 39, 51)]
        [DataRow(6, 42, 21)]
        public void Equal_IsEqual(int hour, int minute, int second)
        {
            TimePeriod tp1 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            TimePeriod tp2 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            Assert.IsTrue(tp1.Equals(tp2));
            Assert.IsTrue(tp2.Equals(tp1));
        }

        [DataTestMethod, TestCategory("Equatable")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(21,48,15,  8,17,58)]
        [DataRow(16,21,34,  15,8,23)]
        public void Equal_IsNotEqual(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsFalse(tp1.Equals(tp2));
            Assert.IsFalse(tp2.Equals(tp1));
        }

        #endregion


        #region Comparable ====================================================

        [TestMethod, TestCategory("Comparable")]
        public void CompareTo_SameReference()
        {
            TimePeriod tp = new TimePeriod(16, 5, 17);
            Assert.AreEqual(tp.CompareTo(tp), 0);
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0, 0, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 0)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(1, 1, 1)]
        public void CompareTo_IsEqual(int hour, int minute, int second)
        {
            TimePeriod tp1 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            TimePeriod tp2 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            Assert.IsTrue(tp1.CompareTo(tp2) == 0);
            Assert.IsTrue(tp2.CompareTo(tp1) == 0);
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void CompareTo_IsBigger(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsTrue(tp2.CompareTo(tp1) > 0);
        }

        [DataTestMethod, TestCategory("Comparable")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void CompareTo_IsSmaller(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsTrue(tp1.CompareTo(tp2) < 0);
        }

        #endregion


        #region Operators ====================================================

        [TestMethod, TestCategory("Operators")]
        public void Operator_Equal_NotEqual_SameReference()
        {
            TimePeriod tp1 = new TimePeriod(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(tp1 == tp1);
            Assert.IsFalse(tp1 != tp1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 0)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(1, 1, 1)]
        public void Operator_Equal_NotEqual_IsEqual(int hour, int minute, int second)
        {
            TimePeriod tp1 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            TimePeriod tp2 = new TimePeriod((byte)hour, (byte)minute, (byte)second);
            Assert.IsTrue(tp1 == tp2);
            Assert.IsTrue(tp2 == tp1);
            Assert.IsFalse(tp1 != tp2);
            Assert.IsFalse(tp2 != tp1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_Equal_NotEqual_IsNotEqual(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsTrue(tp1 != tp2);
            Assert.IsTrue(tp2 != tp1);
            Assert.IsFalse(tp1 == tp2);
            Assert.IsFalse(tp2 == tp1);
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThan_LessThan_SameReference()
        {
            TimePeriod tp1 = new TimePeriod(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsFalse(tp1 > tp1);
            Assert.IsFalse(tp1 < tp1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_GreaterThan_LessThan(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsTrue(tp1 < tp2);
            Assert.IsFalse(tp2 < tp1);
            Assert.IsFalse(tp1 > tp2);
            Assert.IsTrue(tp2 > tp1);
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_SameReference()
        {
            TimePeriod tp1 = new TimePeriod(16, 5, 17);
            #pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(tp1 >= tp1);
            Assert.IsTrue(tp1 <= tp1);
            #pragma warning restore CS1718 // Comparison made to same variable
        }

        [TestMethod, TestCategory("Operators")]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_IsEqual()
        {
            TimePeriod tp1 = new TimePeriod(16, 5, 17);
            TimePeriod tp2 = new TimePeriod(16, 5, 17);
            Assert.IsTrue(tp1 >= tp2);
            Assert.IsTrue(tp2 >= tp1);
            Assert.IsTrue(tp1 <= tp2);
            Assert.IsTrue(tp2 <= tp1);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0,0,0,     0,0,1)]
        [DataRow(0,0,0,     0,1,0)]
        [DataRow(0,0,0,     0,1,1)]
        [DataRow(0,0,0,     1,0,0)]
        [DataRow(0,0,0,     1,0,1)]
        [DataRow(0,0,0,     1,1,0)]
        [DataRow(0,0,0,     1,1,1)]
        [DataRow(14,34,12,  18,17,5)]
        [DataRow(6,14,35,   8,46,7)]
        [DataRow(15,57,27,  21,29,13)]
        public void Operator_GreaterThanOrEqual_LessThanOrEqual_IsDifferent(int h1, int m1, int s1, int h2, int m2, int s2)
        {
            TimePeriod tp1 = new TimePeriod((byte)h1, (byte)m1, (byte)s1);
            TimePeriod tp2 = new TimePeriod((byte)h2, (byte)m2, (byte)s2);
            Assert.IsTrue(tp1 <= tp2);
            Assert.IsFalse(tp2 <= tp1);
            Assert.IsFalse(tp1 >= tp2);
            Assert.IsTrue(tp2 >= tp1);
        }

        #endregion
    }
}