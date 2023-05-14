using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeriod.Structures
{
    /// <summary>
    /// Structure representing a point in time, as hour, minute, and second
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        #region <<< Variables >>>

        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        #endregion


        #region <<< Properties >>>

        /// <summary>
        /// Property containing the hour of a point in time
        /// </summary>
        /// <value>Hours property represents the hour for this instance</value>
        /// <returns>byte</returns>
        public byte Hours { get { return _hours; } }
        /// <summary>
        /// Property containing the minutes of a point in time
        /// </summary>
        /// <value>Minute property represents the minute for this instance</value>
        /// <returns>byte</returns>
        public byte Minutes { get { return _minutes; } }
        /// <summary>
        /// Property containing the minutes of a point in time
        /// </summary>
        /// <value>Seconds property represents the second for this instance</value>
        /// <returns>byte</returns>
        public byte Seconds { get { return _seconds; } }

        #endregion


        #region <<< Constructors >>>

        /// <summary>
        /// Creates an instance with 0 for hour, minute, and second
        /// </summary>
        public Time()
        {
            _hours = 0;
            _minutes = 0;
            _seconds = 0;
        }
        
        /// <summary>
        /// Creates an instance with hour, minute, and second
        /// </summary>
        /// <param name="aHours">Int in range (0-23), representing the hour</param>
        /// <param name="aMinutes">Int in range (0-59), representing the minute</param>
        /// <param name="aSeconds">Int in range (0-59), representing the second</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Time(int aHours, int aMinutes, int aSeconds)
        {
            _hours = ValidateHours(aHours);
            _minutes = ValidateMinutesOrSeconds(aMinutes);
            _seconds = ValidateMinutesOrSeconds(aSeconds);
        }
        
        /// <summary>
        /// Creates an instance with hour, minute, and 0 for second
        /// </summary>
        /// <param name="aHours">Int in range (0-23), representing the hour</param>
        /// <param name="aMinutes">Int in range (0-59), representing the minute</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Time(int aHours, int aMinutes) : this(aHours, aMinutes, 0) { }
        
        /// <summary>
        /// Creates an instance with hour, and 0 for minute and second
        /// </summary>
        /// <param name="aHours">Int in range (0-23), representing the hour</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Time(int aHours) : this(aHours, 0, 0) { }

        /// <summary>
        /// Creates an instance using a string in a correct format <para/>
        /// Correct formats: <br/>
        /// <example>HH:MM:SS</example> <br/>
        /// <example>HH:MM</example>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public Time(string text)
        {
            if(text is null || text == "")
                throw new ArgumentNullException();
            text = text.Trim();
            if (text == "")
                throw new ArgumentNullException();

            string[] splitText = text.Split(':', StringSplitOptions.RemoveEmptyEntries);
            if (splitText.Length > 3 || splitText.Length < 2)
                throw new FormatException();

            _hours = ValidateHours(int.Parse(splitText[0]));
            _minutes = ValidateMinutesOrSeconds(int.Parse(splitText[1]));
            if(splitText.Length == 3)
                _seconds = ValidateMinutesOrSeconds(int.Parse(splitText[2]));
            else
                _seconds = 0;
        }

        /// <summary>
        /// Creates an instance using hour, minute, and second from given DateTime
        /// </summary>
        public Time(DateTime dateTime)
        {
            _hours = (byte)dateTime.Hour;
            _minutes = (byte)dateTime.Minute;
            _seconds = (byte)dateTime.Second;
        }

        /// <summary>
        /// Creates an instance using hour, minute, and second from given DateTimeOffset
        /// </summary>
        public Time(DateTimeOffset dateTimeOffset)
        {
            _hours = (byte)dateTimeOffset.Hour;
            _minutes = (byte)dateTimeOffset.Minute;
            _seconds = (byte)dateTimeOffset.Second;
        }

        private static byte ValidateHours(int hours)
        {
            if (hours > 23 || hours < 0)
                throw new ArgumentOutOfRangeException();
            return (byte)hours;
        }
        private static byte ValidateMinutesOrSeconds(int minutesOrSeconds)
        {
            if (minutesOrSeconds > 59 || minutesOrSeconds < 0)
                throw new ArgumentOutOfRangeException();
            return (byte)minutesOrSeconds;
        }

        #endregion


        #region <<< ToString >>>

        /// <summary>
        /// Returns a text representation of point in time as string in format HH:MM:SS
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return ($"{_hours.ToString("D2")}:{_minutes.ToString("D2")}:{_seconds.ToString("D2")}");
        }

        #endregion


        #region <<< Equatable >>>

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if(obj is Time)
                return Equals((Time)obj);
            return false;
        }
        public bool Equals(Time other)
        {
            if( (_hours != other.Hours) || (_minutes != other.Minutes) || (_seconds != other.Seconds) )
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine( _hours, _minutes, _seconds );
        }

        #endregion


        #region <<< Comparable >>>

        public int CompareTo(Time other)
        {
            int result = _hours.CompareTo(other.Hours);
            if( result != 0 )
                return result;

            result = _minutes.CompareTo(other.Minutes);
            if (result != 0)
                return result;

            result = _seconds.CompareTo(other.Seconds);
            return result;
        }

        #endregion


        #region <<< Operators >>>

        public static bool operator ==(Time t1, Time t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(Time t1, Time t2)
        {
            return !t1.Equals(t2);
        }
        public static bool operator >(Time t1, Time t2)
        {
            return t1.CompareTo(t2) > 0 ? true : false;
        }
        public static bool operator <(Time t1, Time t2)
        {
            return t1.CompareTo(t2) < 0 ? true : false;
        }
        public static bool operator >=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) >= 0 ? true : false;
        }
        public static bool operator <=(Time t1, Time t2)
        {
            return t1.CompareTo(t2) <= 0 ? true : false;
        }

        /// <summary>
        /// Returns a new instance of Time after adding time to the given starting point.
        /// </summary>
        /// <param name="t">Starting point in time</param>
        /// <param name="tp">Added time</param>
        /// <returns>Time</returns>
        public static Time operator +(Time t, TimePeriod tp) => t.Plus(tp);

        /// <summary>
        /// Returns a new instance of Time after subtracting time from the given starting point.
        /// </summary>
        /// <param name="t">Starting point in time</param>
        /// <param name="tp">Subtracted time</param>
        /// <returns>Time</returns>
        public static Time operator -(Time t, TimePeriod tp) => t.Minus(tp);

        #endregion


        #region <<< Plus/Minus >>>

        /// <summary>
        /// Returns a new instance of Time after adding time to the given starting point.
        /// </summary>
        /// <param name="tp">Added time</param>
        /// <returns>Time</returns>
        public Time Plus(TimePeriod tp)
        {
            long totalSeconds = new TimePeriod(Hours, Minutes, Seconds).DurationInSeconds;

            if(tp.DurationInSeconds < (60*60*24))//avoid overflow
                totalSeconds = totalSeconds + tp.DurationInSeconds;
            else
                totalSeconds = totalSeconds + (tp.DurationInSeconds % (60 * 60 * 24));

            if (totalSeconds >= (60 * 60 * 24))
                totalSeconds = totalSeconds % (60 * 60 * 24);
            TimePeriod temp = new TimePeriod(totalSeconds);
            return new Time(temp.Hours, temp.Minutes, temp.Seconds);

        }

        /// <summary>
        /// Returns a new instance of Time after subtracting time from the given starting point.
        /// </summary>
        /// <param name="tp">Subtracted time</param>
        /// <returns>Time</returns>
        public Time Minus(TimePeriod tp)
        {
            long totalSeconds = new TimePeriod(Hours, Minutes, Seconds).DurationInSeconds;
            totalSeconds = totalSeconds - tp.DurationInSeconds;
            while (totalSeconds < 0)
                totalSeconds = totalSeconds + (60 * 60 * 24);
            if(totalSeconds >= (60 * 60 * 24))
                totalSeconds = totalSeconds % (60 * 60 * 24);
            TimePeriod temp = new TimePeriod(totalSeconds);
            return new Time(temp.Hours, temp.Minutes, temp.Seconds);
        }

        /// <summary>
        /// Returns a new instance of Time after adding time to the given starting point.
        /// </summary>
        /// <param name="t">Starting point in time</param>
        /// <param name="tp">Added time</param>
        /// <returns>Time</returns>
        public static Time Plus(Time t, TimePeriod tp)
        {
            return t.Plus(tp);
        }

        /// <summary>
        /// Returns a new instance of Time after subtracting time from the given starting point.
        /// </summary>
        /// <param name="t">Starting point in time</param>
        /// <param name="tp">Subtracted time</param>
        /// <returns>Time</returns>
        public static Time Minus(Time t, TimePeriod tp)
        {
            return t.Minus(tp);
        }

        #endregion
    }
}
