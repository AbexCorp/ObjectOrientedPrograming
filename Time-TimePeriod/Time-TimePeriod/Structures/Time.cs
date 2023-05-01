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
        public Time(int aHours)
        {
            _hours = (byte)aHours;
            _minutes = 0;
            _seconds = 0;
        }
        //public Time() { } Two parameters
        //public Time() { } One parameter
        //public Time() { } String
        //public Time() { } DateTime
        //public Time() { } DateTimeOffset
        private static void ValidateHours(int hours)
        {
            if (hours > 23 || hours < 0)
                throw new ArgumentOutOfRangeException("hours");
        }

    }
}
