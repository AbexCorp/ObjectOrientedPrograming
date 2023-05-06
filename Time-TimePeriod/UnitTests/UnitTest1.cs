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
        [DataRow("00:00:00",    (byte)0,  (byte)0,  (byte)0)]
        [DataRow("23:59:59",    (byte)23, (byte)59, (byte)59)]
        [DataRow("23:59:00",    (byte)23, (byte)59, (byte)0)]
        [DataRow("07:23:05",    (byte)7,  (byte)23, (byte)5)]
        [DataRow("14:03:34",    (byte)14, (byte)3,  (byte)34)]
        [DataRow("03:23:00",    (byte)3,  (byte)23, (byte)0)]
        [DataRow("15:09:41",    (byte)15, (byte)9,  (byte)41)]
        public void ToString(string text, byte Hours, byte Minutes, byte Seconds)
        {
            Time t = new Time(Hours, Minutes, Seconds);
            Assert.AreEqual(text, t.ToString());
        }

        [DataTestMethod, TestCategory("ToString")]
        [DataRow((byte)0,  (byte)0,  (byte)0)]
        [DataRow((byte)23, (byte)59, (byte)59)]
        [DataRow((byte)23, (byte)59, (byte)0)]
        [DataRow((byte)7,  (byte)23, (byte)5)]
        [DataRow((byte)14, (byte)3,  (byte)34)]
        [DataRow((byte)3,  (byte)23, (byte)0)]
        [DataRow((byte)15, (byte)9,  (byte)41)]
        public void ToString_Pass_String_Constructor(byte Hours, byte Minutes, byte Seconds)
        {
            Time t1 = new Time(Hours, Minutes, Seconds);
            Time t2 = new Time(t1.ToString());
            AssertTime(t2, Hours, Minutes, Seconds);
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
}