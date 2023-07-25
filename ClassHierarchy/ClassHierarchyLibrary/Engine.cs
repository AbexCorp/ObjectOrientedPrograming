using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public class Engine
    {
        public enum EngineFuelType { Oil, Gasoline, Electric, Lpg, Coal };


        private static readonly int s_minimumHorsepower = 1;
        private static readonly int s_maximumHorsepower = 100000;

        private int _horsePower;
        private EngineFuelType _fuelType;


        public int HorsePower { get { return _horsePower; } }
        public EngineFuelType FuelType { get { return _fuelType; } }


        public Engine(int horsepower, EngineFuelType fuelType)
        {
            if (horsepower < s_minimumHorsepower || horsepower > s_maximumHorsepower)
                throw new ArgumentException("Horsepower out of range");
            _horsePower = horsepower;
            _fuelType = fuelType;
        }
    }
}
