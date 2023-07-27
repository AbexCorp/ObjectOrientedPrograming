using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public abstract class LandVehicle : Vehicle
    {
        protected int _numberOfWheels;
        public int NumberOfWheels { get { return _numberOfWheels; } }


        public LandVehicle(int numberOfWheels, Engine? engine = null) : base(engine: engine)
        {
            _numberOfWheels = numberOfWheels;

            CurrentLocation = Location.Land;
            CurrentSpeedUnit = SpeedUnit.KilometerPerHour;
        }
    }
}
