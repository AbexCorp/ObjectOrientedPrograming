using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeriod.Structures
{
    struct Time
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
            if (splitText.Length != 3)
                throw new ArgumentException();

            _hours = ValidateHours(int.Parse(splitText[0]));
            _minutes = ValidateHours(int.Parse(splitText[1]));
            _seconds = ValidateHours(int.Parse(splitText[2]));
        }
        public Time(DateTime dateTime)
        {
            _hours = (byte)dateTime.Hour;
            _minutes = (byte)dateTime.Minute;
            _seconds = (byte)dateTime.Millisecond;
        }
        public Time(DateTimeOffset dateTimeOffset)
        {
            _hours = (byte)dateTimeOffset.Hour;
            _minutes = (byte)dateTimeOffset.Minute;
            _seconds = (byte)dateTimeOffset.Millisecond;
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

    }
}
