using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeriod.Structures
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        // <<< Variables >>>
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;


        // <<< Properties >>>
        public byte Hours { get { return _hours; } }
        public byte Minutes { get { return _minutes; } }
        public byte Seconds { get { return _seconds; } }



        // <<< Constructors >>>
        public Time()
        {
            _hours = 0;
            _minutes = 0;
            _seconds = 0;
        }
        public Time(int aHours, int aMinutes, int aSeconds)
        {
            _hours = ValidateHours(aHours);
            _minutes = ValidateMinutesOrSeconds(aMinutes);
            _seconds = ValidateMinutesOrSeconds(aSeconds);
        }
        public Time(int aHours, int aMinutes) 
        {
            _hours = ValidateHours(aHours);
            _minutes = ValidateMinutesOrSeconds(aMinutes);
            _seconds = 0;
        }
        public Time(int aHours)
        {
            _hours = ValidateHours(aHours); ;
            _minutes = 0;
            _seconds = 0;
        }
        public Time(string text)
        {
            if(text is null || text == "")
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
        public Time(DateTime dateTime)
        {
            _hours = (byte)dateTime.Hour;
            _minutes = (byte)dateTime.Minute;
            _seconds = (byte)dateTime.Second;
        }
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



        // <<< ToString >>>
        public override string ToString()
        {
            return ($"{_hours.ToString("D2")}:{_minutes.ToString("D2")}:{_seconds.ToString("D2")}");
        }



        // <<< Equatable >>>
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



        // <<< Comparable >>>
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



        // <<< Operators >>>
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

        //Plus
        //Minus
        //Rest?
    }
}
