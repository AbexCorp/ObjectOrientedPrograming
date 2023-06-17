using System;
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
            int numberOfTest = int.Parse(Console.ReadLine());
            string[] entrySentence = new string[numberOfTest];
            string[] entryQuery = new string[numberOfTest];


            for(int i = 0; i < numberOfTest; i++)
            {
                entrySentence[i] = Console.ReadLine();
                entryQuery[i] = Console.ReadLine();
            }


            for (int i = 0; i < numberOfTest; i++)
            {
                QueryRecognition(entryQuery[i], CountLetters(entrySentence[i]));
                if(i != numberOfTest - 1)
                    Console.WriteLine();
            }
            
        }

        private static SortedDictionary<char, int> CountLetters(string text)
        {
            text = text.ToLower();
            var cleanedText = text.ToCharArray()
                .Where(x => char.IsLetter(x));
            HashSet<char> allChars = new HashSet<char>(cleanedText);


            SortedDictionary<char, int> countedChars = new SortedDictionary<char, int>();
            foreach(char c in allChars)
            {
                countedChars.Add(c, text.Count( x => x == c));
            }
            return countedChars;
        }

        private static void QueryRecognition(string text, SortedDictionary<char, int> countedChars)
        {
            //first 3 byfreq asc byletter asc
            //last 2 byletter desc
            string[] queryText = text.Split(' ');
            int numberOfRecords = int.Parse(queryText[1]);

            var query = countedChars.Select(c => c);
            if( queryText.Length == 4 )
            {
                if (queryText[3] == "asc")
                    query = query.OrderBy(x => queryText[2] == "byletter" ? x.Key : x.Value);

                if (queryText[3] == "desc")
                    query = query.OrderByDescending(x => queryText[2] == "byletter" ? x.Key : x.Value);
            }
            else
            {
                if (queryText[3] == "asc")
                {
                    if (queryText[5] == "asc")
                    {
                        query = query
                            .OrderBy(x => queryText[2] == "byletter" ? x.Key : x.Value)
                            .ThenBy(x => queryText[4] == "byletter" ? x.Key : x.Value);
                    }
                    if (queryText[5] == "desc")
                    {
                        query = query
                            .OrderBy(x => queryText[2] == "byletter" ? x.Key : x.Value)
                            .ThenByDescending(x => queryText[4] == "byletter" ? x.Key : x.Value);
                    }
                }
                if (queryText[3] == "desc")
                {
                    if (queryText[5] == "asc")
                    {
                        query = query
                            .OrderByDescending(x => queryText[2] == "byletter" ? x.Key : x.Value)
                            .ThenBy(x => queryText[4] == "byletter" ? x.Key : x.Value);
                    }
                    if (queryText[5] == "desc")
                    {
                        query = query
                            .OrderByDescending(x => queryText[2] == "byletter" ? x.Key : x.Value)
                            .ThenByDescending(x => queryText[4] == "byletter" ? x.Key : x.Value);
                    }
                }
            }

            if (queryText[0] == "first")
                query = query.Take(numberOfRecords);
            else
                query = query.TakeLast(numberOfRecords);


            int counter = 1;
            foreach(var letter in query)
            {
                if (counter > numberOfRecords)
                    break;
                Console.WriteLine($"{letter.Key} {letter.Value}");
                counter++;
            }
        }
    }
}