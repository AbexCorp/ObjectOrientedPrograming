/*

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
            string test = Console.ReadLine();
            string entryString = Console.ReadLine();

            
            var query = entryString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(songTime => songTime.Trim())
                .Select(songTime => songTime.Split(':'))
                .Select(song => (Minute: song[0], Second: song[1]));

            List<Lap> lapList = new List<Lap>();
            foreach ( var song in query ) 
            {
                lapList.Add(new Lap { TotalTime = int.Parse(song.Minute)*60 + int.Parse(song.Second) });
            }

            int lapCount = lapList.Count();

            int slowestLapIndex = 0;
            int slowestLapCount = 0;
            int fastestLapIndex = 0;
            int fastestLapCount = 0;

            int totalRunTime = 0;


            int previousLaps = 0;
            for(int i = 0; i < lapCount; i++) 
            {
                lapList[i].TotalTime -= previousLaps;
                previousLaps += lapList[i].TotalTime;
            }


            for(int i = 0; i < lapCount; i++)
            {
                if (lapList[i].TotalTime < lapList[fastestLapIndex].TotalTime)
                {
                    fastestLapIndex = i;
                    fastestLapCount++;
                }
                if (lapList[i].TotalTime > lapList[slowestLapIndex].TotalTime)
                {
                    slowestLapIndex = i;
                    slowestLapCount++;
                }
                totalRunTime += lapList[i].TotalTime;
            }
            Lap averageLapTime = new Lap { TotalTime = (int)Math.Ceiling(((double)totalRunTime / lapCount)) };




            
            switch(test)
            {
                case "test1":
                    Console.WriteLine(lapCount);
                    break;


                case "test2":
                    Console.WriteLine(AllLaps(lapList, lapCount));
                    break;


                case "test3":
                    string laps = "";
                    if(fastestLapCount > 1)
                    {
                        for(int i = 0; i < lapCount; i++)
                        {
                            if (lapList[i].TotalTime == lapList[fastestLapIndex].TotalTime)
                                laps += $" {i + 1}";
                        }
                    }
                    else
                        laps = (fastestLapIndex+1).ToString();
                    laps = laps.Trim();

                    Console.WriteLine($"{lapList[fastestLapIndex].Minute:D2}:{lapList[fastestLapIndex].Second:D2} {laps}");
                    break;


                case "test4":
                    laps = "";
                    if(slowestLapCount > 1)
                    {
                        for(int i = 0; i < lapCount; i++)
                        {
                            if (lapList[i].TotalTime == lapList[slowestLapIndex].TotalTime)
                                laps += $" {i + 1}";
                        }
                    }
                    else
                        laps = (slowestLapIndex+1).ToString();
                    laps = laps.Trim();

                    Console.WriteLine($"{lapList[slowestLapIndex].Minute:D2}:{lapList[slowestLapIndex].Second:D2} {slowestLapIndex+1}");
                    break;


                case "test5":
                    Console.WriteLine($"{averageLapTime.Minute:D2}:{averageLapTime.Second:D2}");
                    break;
            }
        }

        private static string AllLaps(List<Lap> lapList, int lapCount)
        {
            string output = "";
            for (int i = 0; i < lapCount; i++)
            {
                    output += $"{lapList[i].Minute:D2}:{lapList[i].Second:D2} ";
            }
            return output.Trim();
        }

        public class Lap
        {
            public int TotalTime { get; set; }

            public int Minute { get { return TotalTime/60; } }
            public int Second { get { return TotalTime%60; } }
        }
    }
}

*/