using PlayerPosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerPositionLib
{
    public class Player
    {
        public class PlayerEventArgs : EventArgs
        {
            public int PlayerId { get; }
            public int OldX { get; }
            public int OldY { get; }
            public int NewX { get; }
            public int NewY { get; }
            public PlayerEventArgs(int playerId, int oldX, int oldY, int newX, int newY)
            {
                PlayerId = playerId; OldX = oldX; OldY = oldY; NewX = newX; NewY = newY;
            }
        }
        public event EventHandler LocationChanged = null;


        private static int uniqueId = 1;

        private int _id;
        private Location _currentLocation;


        public int Id { get { return _id; } }
        public Location CurrentLocation { get { return _currentLocation; } }




        public Player(int fieldHeight, int fieldWidth, int x, int y, LocationTracker lt)
        {
            _id = uniqueId;
            uniqueId++;
            _currentLocation = new Location(fieldHeight, fieldWidth, x, y);
            LocationChanged += lt.OnLocationChange;
        }




        public void Move()
        {
            Random rng = new Random();
            ChangeLocation( rng.Next(-10,11), rng.Next(-10,11) );
        }
        public void ChangeLocation(int x, int y)
        {
            if (x < -10 || x > 10 || y < -10 || y > 10)
                throw new ArgumentException("Moving too fast");

            int oldX = _currentLocation.X;
            int oldY = _currentLocation.Y;
            _currentLocation.X = x;
            _currentLocation.Y = y;

            PlayerEventArgs movement = new PlayerEventArgs(Id, oldX, oldY, _currentLocation.X, _currentLocation.Y);
            LocationChanged?.Invoke(this, movement);
        }

        public override string ToString()
        {
            return $"{_id}: {_currentLocation}    {CurrentLocation.Time}";
        }
    }
}
