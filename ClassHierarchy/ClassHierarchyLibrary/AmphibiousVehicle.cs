using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassHierarchyLibrary.ComboVehicleInterfaces;

namespace ClassHierarchyLibrary
{
    public abstract class AmphibiousVehicle : LandVehicle, ILandToWater
    {
        protected int _buoyancy;
        public int Buoyancy { get { return _buoyancy; } }

        public void Dock()
        {
            if (!IsInWater)
                return;

            CurrentLocation = Location.Land;
            CurrentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.KilometerPerHour, CurrentSpeed) ;
            CurrentSpeedUnit = SpeedUnit.KilometerPerHour;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }
        public void Sail()
        {
            if (!IsOnLand)
                return;

            CurrentLocation = Location.Water;
            CurrentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.Knots, CurrentSpeed) ;
            CurrentSpeedUnit = SpeedUnit.Knots;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }
    }
}
