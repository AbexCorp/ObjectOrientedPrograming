/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_to
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Generator
            //foreach(var x in LiczbyCalkowiteNaprzemiennie(10))
            //{
            //  Console.Write($"{x} ");
            //}

            //LINQ to Object Sort by date
            //var napis = " Krzysztof  Molenda,  1987-01-01; Jan Kowalski, 1987-01-01; Anna Abacka, 1972-05-20; Józef Kabacki, 2000-01-02; Kazimierz Moksa, 2001-01-02";
            //var wynik = SortByDateOfBirthThenLastName(napis);
            //Console.WriteLine(wynik);
            
        }


        public static IEnumerable<int> LiczbyCalkowiteNaprzemiennie(int n) //Generator
        {
            yield return 0;
            int value = 1;
            for(int i = 0; i < n; i++)
            {
                yield return value;
                if (value > 0)
                    value++;
                else
                    value--;
                value = value * -1;
            }
        }

        static string SortByDateOfBirthThenLastName(string napis) //LINQ to Object Sort by date
        {
            var query = napis.Trim().Split(';')
                .Select(person => person.Trim())
                .Select(osoba => osoba.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .Select(osoba => (imie: osoba[0].Trim(), nazwisko: osoba[1].Trim(), data: osoba[2].Trim()))
                .OrderBy(osoba => osoba.data)
                .ThenBy(osoba => osoba.nazwisko)
                .Select(osoba => osoba.imie + " " + osoba.nazwisko + " " + osoba.data);
            return string.Join("; ", query);
        }
    }
}
*/
