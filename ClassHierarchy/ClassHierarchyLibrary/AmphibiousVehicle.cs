using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassHierarchyLibrary.ComboVehicleInterfaces;

namespace ClassHierarchyLibrary
{
    public class AmphibiousVehicle : LandVehicle, ILandToWater
    {
        protected int _buoyancy;

        public int Buoyancy { get { return _buoyancy; } }

        public void Dock()
        {
            if (IsOnLand)
                return;

            _location = Location.Land;
            _currentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.KilometerPerHour, CurrentSpeed) ;
            _currentSpeedUnit = SpeedUnit.KilometerPerHour;
            _currentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }
        public void Sail()
        {
            if (IsInWater)
                return;

            _location = Location.Water;
            _currentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.Knots, CurrentSpeed) ;
            _currentSpeedUnit = SpeedUnit.Knots;

        }


        public override void Start()
        {
            if (IsInMotion)
                return;

            _state = VehicleState.Moving;
            _currentSpeed = 1;
        }
        public override void Stop()
        {
            if (IsStationary)
                return;

            _state = VehicleState.Stationary;
            _currentSpeed = 0;
        }
        public override void ChangeSpeed(int speedChange)
        {
            if (IsStationary)
                return;
            _currentSpeed += speedChange;
            if(_currentSpeed > s_maximumAllowedLandSpeed)
                _currentSpeed = s_maximumAllowedLandSpeed;
            if (_currentSpeed < s_minimumAllowedLandSpeed)
                _currentSpeed = s_minimumAllowedLandSpeed;
        }
    }
}
