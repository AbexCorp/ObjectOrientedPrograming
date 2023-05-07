using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimeTimePeriod.Structures;

namespace TimeTimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            Time t1 = new Time("00:00");
            Console.WriteLine(t1);
            Time t2 = new Time("00:00");
            Console.WriteLine(t2);
            Console.WriteLine();
            TimePeriod tp = new TimePeriod(t1,t2); //Small to Big
            Console.WriteLine(tp);
            tp = new TimePeriod(t2,t1); //Big to small
            Console.WriteLine(tp);
        }
    }
}