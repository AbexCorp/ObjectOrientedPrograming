using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeriod.Structures
{
    /// <summary>
    /// Structure representing a period of time
    /// </summary>
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        #region <<< Variables >>>

        private readonly long _seconds;

        #endregion


        #region <<< Properties >>>

        /// <summary>
        /// Property containing the hours of a period of time
        /// </summary>
        /// <value>Hours property represents the hour for this instance</value>
        /// <returns>int</returns>
        public int Hours
        {
            get { return ( (int)Math.Truncate(_seconds / 3600d) ); }
        }

        /// <summary>
        /// Property containing the minutes of a period of time
        /// </summary>
        /// <value>Minutes property represents the minute for this instance</value>
        /// <returns>byte</returns>
        public byte Minutes
        {
            get { return ( (byte)Math.Truncate((_seconds - (Hours * 3600d))/60d) ); }
        }

        /// <summary>
        /// Property containing the seconds of a period of time
        /// </summary>
        /// <value>Seconds property represents the second for this instance</value>
        /// <returns>byte</returns>
        public byte Seconds
        {
            get { return ( (byte)(_seconds - ((Hours * 3600) + (Minutes * 60)) )); }
        }

        /// <summary>
        /// Property representing the period of time as string
        /// </summary>
        /// <value>Duration property represents the period of time as a string in a HH:MM:SS format</value>
        /// <returns>string</returns>
        public string Duration
        {
            get { return ($"{Hours.ToString("d2")}:{Minutes.ToString("d2")}:{Seconds.ToString("d2")}"); }
        }

        /// <summary>
        /// Property containing the period of time in seconds
        /// </summary>
        /// <value>DurationInSeconds property represents this instance in seconds</value>
        /// <returns>long</returns>
        public long DurationInSeconds { get { return _seconds; } }

        /// <summary>
        /// Maximum value that TimePeriod can store. Equal to 255:59:59
        /// </summary>
        public int MaxValue => 921599;

        #endregion


        #region <<< Constructors >>>

        /// <summary>
        /// Creates an instance with 0 seconds;
        /// </summary>
        public TimePeriod()
        {
            _seconds = 0;
        }

        /// <summary>
        /// Creates an instance with hours, minutes, and seconds;
        /// </summary>
        /// <param name="hours">byte in range (0-23), representing the hour</param>
        /// <param name="minutes">byte in range (0-59), representing the minute</param>
        /// <param name="seconds">byte in range (0-59), representing the second</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod( byte hours, byte minutes, byte seconds)
        {
            if (minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();
            _seconds = 3600 * hours + 60 * minutes + seconds;
        }

        /// <summary>
        /// Creates an instance with hours and minutes;
        /// <param name="hours">byte in range (0-23), representing the hour</param>
        /// <param name="minutes">byte in range (0-59), representing the minute</param>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(byte hours, byte minutes) : this(hours, minutes, 0) { }

        /// <summary>
        /// Creates an instance with seconds;
        /// </summary>
        /// <param name="seconds">byte in range (0-59), representing the second</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public TimePeriod(long seconds)
        {
            if(seconds < 0)
                throw new ArgumentOutOfRangeException();
            _seconds = seconds;
        }

        /// <summary>
        /// Creates an instance from the time between two point in time; <para/>
        /// <example>10:00:15 and 20:00:00 returns 09:59:45 (10:00:15 -> 20:00:00)</example><br/>
        /// <example>20:00:00 and 10:00:15 returns 14:00:15 (20:00:00 -> 10:00:15)</example>
        /// </summary>
        /// <param name="t1">Earlier point in time</param>
        /// <param name="t2">Later point in time</param>
        public TimePeriod(Time t1, Time t2)
        {
            long seconds1 = new TimePeriod(t1.Hours, t1.Minutes, t1.Seconds).DurationInSeconds;
            long seconds2 = new TimePeriod(t2.Hours, t2.Minutes, t2.Seconds).DurationInSeconds;
            if(seconds1 <= seconds2)
                _seconds = Math.Abs(seconds2 - seconds1);
            else
                _seconds = (60*60*24)-seconds1 + seconds2;
        }

        /// <summary>
        /// Creates an instance using a string in a correct format <para/>
        /// Correct formats: <br/>
        /// <example>HH:MM:SS</example> <br/>
        /// <example>HH:MM</example>
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public TimePeriod(string text)
        {
            if(text is null || text == "")
                throw new ArgumentNullException();
            text = text.Trim();
            if(text == "")
                throw new ArgumentNullException();

            byte[] parsedNumbers = Array.ConvertAll(text.Split(':', StringSplitOptions.RemoveEmptyEntries), byte.Parse);
            if (parsedNumbers.Length < 2 || parsedNumbers.Length > 3)
                throw new FormatException();
            
            TimePeriod temp = new TimePeriod();
            temp = parsedNumbers.Length == 3 ? new TimePeriod(parsedNumbers[0], parsedNumbers[1], parsedNumbers[2]) : new TimePeriod(parsedNumbers[0], parsedNumbers[1]);
            _seconds = temp.DurationInSeconds;
        }

        #endregion


        #region <<< ToString >>>

        /// <summary>
        /// Returns a text representation of a period of time as a string in format HH:MM:SS
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return Duration;
        }

        #endregion


        #region <<< Equatable >>>

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }
        public bool Equals(TimePeriod tp)
        {
            if(DurationInSeconds == tp.DurationInSeconds) 
                return true;
            return false;
        }
        public override int GetHashCode()
        {
            return _seconds.GetHashCode();
        }

        #endregion


        #region <<< Comparable >>>

        public int CompareTo(TimePeriod tp)
        {
            return DurationInSeconds.CompareTo(tp.DurationInSeconds);
        }

        #endregion


        #region <<< Operators >>>

        public static bool operator ==(TimePeriod t1, TimePeriod t2)
        {
            return t1.Equals(t2);
        }
        public static bool operator !=(TimePeriod t1, TimePeriod t2)
        {
            return !t1.Equals(t2);
        }
        public static bool operator >(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) > 0 ? true : false;
        }
        public static bool operator <(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) < 0 ? true : false;
        }
        public static bool operator >=(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) >= 0 ? true : false;
        }
        public static bool operator <=(TimePeriod t1, TimePeriod t2)
        {
            return t1.CompareTo(t2) <= 0 ? true : false;
        }

        /// <summary>
        /// Returns a new instance of TimePeriod after adding two time periods.
        /// </summary>
        /// <returns>TimePeriod</returns>
        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Plus(tp2);
        }

        /// <summary>
        /// Returns a new instance of TimePeriod after subtracting one time period from another.
        /// </summary>
        /// <param name="tp1">Time period subtracted from</param>
        /// <param name="tp2">Subtracted period of time</param>
        /// <returns>TimePeriod</returns>
        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Minus(tp2);
        }

        #endregion


        #region <<< Plus/Minus >>>

        /// <summary>
        /// Returns a new instance of TimePeriod after adding two time periods.
        /// </summary>
        /// <returns>TimePeriod</returns>
        public TimePeriod Plus(TimePeriod tp) 
        {
            return new TimePeriod(_seconds + tp.DurationInSeconds);
        }

        /// <summary>
        /// Returns a new instance of TimePeriod after subtracting one time period from another.
        /// </summary>
        /// <param name="tp">Subtracted period of time</param>
        /// <returns>TimePeriod</returns>
        public TimePeriod Minus(TimePeriod tp)
        {
            long seconds = _seconds - tp.DurationInSeconds;
            if(seconds < 0)
                seconds = 0;
            return new TimePeriod(seconds);
        }

        /// <summary>
        /// Returns a new instance of TimePeriod after adding two time periods.
        /// </summary>
        /// <returns>TimePeriod</returns>
        public static TimePeriod Plus(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Plus(tp2);
        }

        /// <summary>
        /// Returns a new instance of TimePeriod after subtracting one time period from another.
        /// </summary>
        /// <param name="tp1">Time period subtracted from</param>
        /// <param name="tp2">Subtracted period of time</param>
        /// <returns>TimePeriod</returns>
        public static TimePeriod Minus(TimePeriod tp1, TimePeriod tp2)
        {
            return tp1.Minus(tp2);
        }

        #endregion
    }
}
