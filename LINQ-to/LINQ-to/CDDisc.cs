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
            string entryString = Console.ReadLine();
            //entryString = "4:12,2:43,3:51,4:29,3:24,3:14,4:46,3:25,4:52,3:27"; //Should return (10 3:51 38:23)

            var query = entryString
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(songTime => songTime.Trim())
                .Select(songTime => songTime.Split(':'))
                .Select(song => (Minute: song[0], Second: song[1]));

            List<DiscSong> songList = new List<DiscSong>();
            foreach ( var song in query ) 
            {
                songList.Add(new DiscSong { Minute = int.Parse(song.Minute), Second = int.Parse(song.Second) });
            }

            int songCount = songList.Count();
            int totalSongTime = 0;
            int averageSongTime;

            foreach ( var song in songList )
            {
                totalSongTime = totalSongTime + song.TotalTime;
            }
            averageSongTime = (int)Math.Ceiling(( (double)totalSongTime / songCount));

            string totalDiscTime = totalSongTime/3600 == 0 ? $"{totalSongTime/60}:{totalSongTime%60:D2}" : $"{totalSongTime/3600}:{(totalSongTime%3600)/60:D2}:{totalSongTime%60:D2}";
            Console.WriteLine($"{songCount} {averageSongTime/60}:{averageSongTime%60:D2} {totalDiscTime}");
        }
        public class DiscSong //LINQ to Object PłytaCD
        {
            public int TotalTime { get { return ((Minute * 60) + Second); } }
            public int Minute { get; set; }
            public int Second { get; set; }
        }
    }
}

*/