using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerPosition;

namespace PlayerPositionLib
{
    public class LocationTracker
    {
        private int _maxX;
        private int _maxY;
        private SortedDictionary<int, Player> _players = new();
        private SortedDictionary<int, Location> _locationList = new();
        private SortedDictionary<int, double> _distanceCovered = new();



        public int FieldHeight { get { return _maxY; } }
        public int FieldWidth { get { return _maxX; } }
        public SortedDictionary<int, Player> PlayerList { get { return _players; } }



        public LocationTracker(int fieldHeight, int fieldWidth)
        { 
            _maxX = fieldWidth;
            _maxY = fieldHeight;
            OnLocationChange = OnLocationChangeDefault;
        }


        public Player AddPlayer(int x, int y)
        {
            Player newPlayer = new Player(FieldHeight, FieldWidth, x, y, this);
            _players.Add(newPlayer.Id, newPlayer);
            _locationList.Add(newPlayer.Id, newPlayer.CurrentLocation);
            _distanceCovered.Add(newPlayer.Id, 0);
            return newPlayer;
        }
        public Player GetPlayerById(int id)
        {
            return _players[id];
        }
        public bool CheckIfPlayerExistsById(int id)
        {
            return _players.ContainsKey(id);
        }
        public double GetPlayerDistanceCoveredById(int id)
        {
            return Math.Round(_distanceCovered[id], 3);
        }
        public int GetIdOfLongestDistanceCoveredPlayer()
        {
            int id = 0;
            double max = -1;
            foreach(var x in _distanceCovered)
            {
                if (x.Value > max)
                {
                    max = x.Value;
                    id = x.Key;
                }
            }
            return id;
        }


        public EventHandler OnLocationChange;
        private void OnLocationChangeDefault(object? sender, EventArgs args)
        {
            Player.PlayerEventArgs args1 = (Player.PlayerEventArgs)args;
            _locationList[args1.PlayerId] = _players[args1.PlayerId].CurrentLocation;
            double distance = Math.Round( Math.Sqrt( (Math.Pow(Math.Abs(args1.OldX - args1.NewX),2)+Math.Pow(Math.Abs(args1.OldY - args1.NewY),2)) ), 3); //Pythagorean theorem
            _distanceCovered[args1.PlayerId] += distance;
        }
    }
}
