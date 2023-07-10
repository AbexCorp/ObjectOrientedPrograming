using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewConcepts.DelegateBasics
{
    /// <summary>
    /// Delegates are used to pass functions as params
    /// </summary>
    /// <example>
    /// object.sort(functionName)
    /// </example>
    /// <see cref="https://e.wsei.edu.pl/pluginfile.php/87633/mod_resource/content/1/01.%20delegates-wprowadzenie-wyklad.html"/>
    /// <see cref="https://e.wsei.edu.pl/pluginfile.php/87634/mod_resource/content/2/02.%20delegates-indepth-wyklad.html"/>
    /// <see cref="https://e.wsei.edu.pl/pluginfile.php/87635/mod_resource/content/3/03.%20from-delegates-to-events-wyklad.html"/>
    public static class Delegates
    {
        public static void AddAnswer()
        {
            Console.WriteLine(Calculator(1, 2, 3, Sum));
            Console.WriteLine(Calculator(1, 2, 3, Sub));
            Console.WriteLine(Calculator(1, 2, 3, Mul));
            Console.WriteLine(Calculator(1, 2, 3, (x, y, z) => y * z - x));
            Console.WriteLine(Calculator(1, 2, 3, (x, y, z) => { return y * z - x; }));
            Console.WriteLine(Calculator(1, 2, 3, delegate (int x, int y, int z) { return x + y - z; }));
        }



        public delegate int Calculate(int x, int y, int z);
        public static int Calculator(int x, int y, int z, Calculate c)
        {
            return c(x, y, z);
        }


        public static int Sum(int x, int y, int z) { return x + y + z; }
        public static int Sub(int x, int y, int z) { return x - y - z; }
        public static int Mul(int x, int y, int z) { return x * y * z; }





        //Converter Delegate
        //public delegate TOutput Converter<in TInput, out TOutput>(TInput input);
        //Used to convert object into another object
        //Ex:
        // array.ConvertAll
        // List<T>.ConvertAll
        public static void ConverterDelegate()
        {
            int[] array = { 1, 2, 3 };
            Console.WriteLine(string.Join(' ', array));
            int[] arrayPlusOne = Array.ConvertAll(array, n => n + 1);
            Console.WriteLine(string.Join(' ', arrayPlusOne));
        }







        //Comparison Delegate
        //public delegate int Comparison<in T>(T x, T y);
        //Used for sorting
        /*      
            // porównanie napisów najpierw wg długości napisu, potem leksykograficznie
            Comparison<string> wgDlugosciPotemLeksykograficznie =
                (x,y) => (x.Length == y.Length)?
                            x.CompareTo(y) :
                            (x.Length).CompareTo(y.Length);

            string[] zwierzaki = {"mucha", "kot", "pies", "ryba", "ptak", "zebra"};
            Array.Sort(zwierzaki); // leksykograficznie
            Console.WriteLine( string.Join(", ", zwierzaki ) );
            Array.Sort(zwierzaki, wgDlugosciPotemLeksykograficznie);
            Console.WriteLine( string.Join(", ", zwierzaki ) );       
        */








        //Predictate Delegate
        //public delegate bool Predicate<in T>(T obj);
        //Used for creating logical conditions. Not recomended anymore
        /*
            Predicate<string> krotszyNiz5 = (s) => s.Length < 5;

            string[] zwierzaki = {"mucha", "kot", "pies", "ryba", "ptak", "zebra"};
            Console.WriteLine( string.Join(", ", Array.FindAll( zwierzaki, krotszyNiz5 ) ) );
        */









        //Func & Action Delegate
        /*FunC
            public delegate TResult Func<out TResult>();
            public delegate TResult Func<in T1, out TResult>(T1 arg1);
            public delegate TResult Func<in T1, in T2, out TResult>(T1 arg1, T2 arg2);
            ...
            public delegate TResult Func<in T1, ..., in T16, out TResult>(T1 arg1, ..., T16 arg16);
        */
        /*Action
            public delegate void Action();
            public delegate void Action<in T1>(T1 arg);
            public delegate void Action<in T1, in T2>(T1 arg1, T2 arg2);
            ...
            public delegate void Action<in T1, ..., in T16>(T1 arg1, ..., T16 arg16);
        */
        //0-16 arguments, Func returns value, Action returns Void

        /*
            Func<int, int, int> f = (x,y) => x + y;
            Console.WriteLine( f(2,3) );
        */
        /*
            List<string> zwierzaki = new List<string>{"mucha", "kot", "pies", "ryba", "ptak", "zebra"};

            Action<string> Wypisz = (s) => Console.Write(s + " ");
            Action<string> Wykrzycz = (s) => Console.Write( s.ToUpper() + " ");

            zwierzaki.ForEach( Wypisz ); Console.WriteLine();
            zwierzaki.ForEach( Wykrzycz ); Console.WriteLine();
        */
    }
}
