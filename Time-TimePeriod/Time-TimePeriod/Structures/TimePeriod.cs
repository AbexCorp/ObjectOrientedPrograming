using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTimePeriod.Structures
{
    public struct TimePeriod
    {
        // <<< Variables >>>
        private readonly long _seconds;


        // <<< Properties >>>
        public int Hours
        {
            get { return ( (int)Math.Truncate(_seconds / 3600d) ); }
        }
        public byte Minutes
        {
            get { return ( (byte)Math.Truncate((_seconds - (Hours * 3600d))/60d) ); }
        }
        public byte Seconds
        {
            get { return ( (byte)(_seconds - ((Hours * 3600) + (Minutes * 60)) )); }
        }
        public string Duration
        {
            get { return ($"{Hours.ToString("d2")}:{Minutes.ToString("d2")}:{Seconds.ToString("d2")}"); }
        }
        public long DurationInSeconds { get { return _seconds; } }



        // <<< Constructors >>>
        public TimePeriod()
        {
            _seconds = 0;
        }
        public TimePeriod( byte hours, byte minutes, byte seconds)
        {
            if (hours < 0 || minutes < 0 || seconds < 0 || minutes > 59 || seconds > 59)
                throw new ArgumentOutOfRangeException();
            _seconds = 3600 * hours + 60 * minutes + seconds;
        }
        public TimePeriod(byte hours, byte minutes) : this(hours, minutes, 0) { }
        public TimePeriod(long seconds)
        {
            if(seconds < 0)
                throw new ArgumentOutOfRangeException();
            _seconds = seconds;
        }
        public TimePeriod(Time t1, Time t2)
        {
            long seconds1 = new TimePeriod(t1.Hours, t1.Minutes, t1.Seconds).DurationInSeconds;
            long seconds2 = new TimePeriod(t2.Hours, t2.Minutes, t2.Seconds).DurationInSeconds;
            _seconds = Math.Abs(seconds2 - seconds1);
        }
        public TimePeriod(string text)
        {
            byte[] parsedNumbers = Array.ConvertAll(text.Split(':', StringSplitOptions.RemoveEmptyEntries), byte.Parse);
            if (parsedNumbers.Length < 2 || parsedNumbers.Length > 3)
                throw new FormatException();
            
            TimePeriod temp = new TimePeriod();
            temp = parsedNumbers.Length == 3 ? new TimePeriod(parsedNumbers[0], parsedNumbers[1]) : new TimePeriod(parsedNumbers[0], parsedNumbers[1], parsedNumbers[2]);
            _seconds = temp.DurationInSeconds;
        }
    }
}
