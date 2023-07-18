using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerPosition
{
    public class Location
    {
        private int _maxX;
        private int _maxY;
        private int _x;
        private int _y;
        private DateTime _time;




        public int X
        { 
            get { return _x; }
            set
            {
                if (_x + value < 0 || _x + value > _maxX)
                    return;
                else _x = _x + value;
                {
                    _time = DateTime.Now;
                }
            }
        }
        public int Y
        {
            get { return _y; }
            set
            {
                if (_y + value < 0 || _y + value > _maxY)
                    return;
                else
                {
                    _y = _y + value;
                    _time = DateTime.Now;
                }
            }
        }
        public DateTime Time
        {
            get { return _time; }
        }



        public Location(int fieldHeight, int fieldWidth, int x, int y)
        {
            _maxX = fieldWidth;
            _maxY = fieldHeight;
            _x = x;
            _y = y;
            _time = DateTime.Now;
        }
        public override string ToString()
        {
            return $"X: {X}  Y:{Y}";
        }
    }
}
