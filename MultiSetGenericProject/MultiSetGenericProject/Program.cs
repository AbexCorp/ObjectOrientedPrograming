using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MultiSetGeneric.Structures;


namespace MultiSetGeneric
{
    class Program
    {
        static void Main(string[] args)
        {
            MultiSet<char> ms1 = new MultiSet<char>();
            ms1.Add('a', 1);
            ms1.Add('b', 2);
            ms1.Add('c', 3);

            MultiSet<char> ms2 = new MultiSet<char>();
            ms2.Add('a');
            ms2.Add('b');
            ms2.Add('c');

            Console.WriteLine(ms1);
            Console.WriteLine(ms2);

            //ms1.UnionWith(ms2); // [a,a,b,b,b,c,c,c,c]
            //ms1.IntersectWith(ms2); // [a,b,c]
            //ms1.ExceptWith(ms2); // [b,c,c]
            //Console.WriteLine(ms1);


            ms1.Clear();
            ms2.Clear();

            ms1.Add('a', 1);
            ms1.Add('b', 1);
            ms1.Add('c', 1);

            ms2.Add('b', 1);
            ms2.Add('c', 1);
            ms2.Add('d', 1);

            ms1.SymmetricExceptWith(ms2);
            Console.WriteLine(ms1);
        }
    }
}