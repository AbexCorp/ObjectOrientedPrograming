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
            Console.WriteLine(ms1.ToStringShort());

            
            
            /*
            Console.WriteLine(ms1);
            Console.WriteLine(ms2);
            Console.WriteLine("------------------------");

            Console.WriteLine("F ms1.IsSubsetOf(ms2)" + ms1.IsSubsetOf(ms2)); //False
            Console.WriteLine("T ms2.IsSubsetOf(ms1)" + ms2.IsSubsetOf(ms1)); //True
            Console.WriteLine("F ms1.IsProperSubsetOf(ms2)" + ms1.IsProperSubsetOf(ms2)); //False
            Console.WriteLine("T ms2.IsProperSubsetOf(ms1)" + ms2.IsProperSubsetOf(ms1)); //True
            Console.WriteLine("T ms1.IsSupersetOf(ms1)" + ms1.IsSupersetOf(ms1)); //True
            Console.WriteLine("F ms2.IsSupersetOf(ms1)" + ms2.IsSupersetOf(ms1)); //False
            Console.WriteLine("T ms1.IsProperSupersetOf(ms2)" + ms1.IsProperSupersetOf(ms2)); //True
            Console.WriteLine("F ms2.IsProperSupersetOf(ms1)" + ms2.IsProperSupersetOf(ms1)); //False

            ms1.Clear();
            ms2.Clear();

            ms1.Add('a');
            ms1.Add('b');

            ms2.Add('a');
            ms2.Add('b');

            Console.WriteLine(ms1);
            Console.WriteLine(ms2);
            Console.WriteLine("------------------------");

            Console.WriteLine("F ms1.IsProperSubsetOf(ms2)" + ms1.IsProperSubsetOf(ms2)); //False
            Console.WriteLine("F ms2.IsProperSubsetOf(ms1)" + ms2.IsProperSubsetOf(ms1)); //False
            Console.WriteLine("F ms1.IsProperSupersetOf(ms2)" + ms1.IsProperSupersetOf(ms2)); //False
            Console.WriteLine("F ms2.IsProperSupersetOf(ms1)" + ms2.IsProperSupersetOf(ms1)); //False
            */


            /*
            Console.WriteLine(ms1.MultiSetEquals(ms2)); //F
            Console.WriteLine(ms2.MultiSetEquals(ms1)); //F
            ms1.Clear();
            ms2.Clear();

            ms1.Add('a');
            ms1.Add('b');

            ms2.Add('a');
            ms2.Add('b');
            Console.WriteLine(ms1.MultiSetEquals(ms2)); //T
            Console.WriteLine(ms2.MultiSetEquals(ms1)); //T
            */

            /*
            ms1.Clear();
            ms2.Clear();

            ms1.Add('a');
            ms1.Add('b');
            ms1.Add('c');

            ms2.Add('d');
            ms2.Add('e');

            Console.WriteLine(ms1.Overlaps(ms2)); //F
            Console.WriteLine(ms2.Overlaps(ms1)); //F

            ms2.Add('c');

            Console.WriteLine(ms1.Overlaps(ms2)); //T
            Console.WriteLine(ms2.Overlaps(ms1)); //T
            */


        }
    }
}