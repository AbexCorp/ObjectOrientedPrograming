using ClassHierarchyLibrary.ComboVehicleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public class Aircraft : LandVehicle, ILandToAir
    {
        public void Landing()
        {
            if (!IsFlying || IsStationary)
                return;
            if (ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.KilometerPerHour, CurrentSpeed) > s_maximumAllowedLandSpeed)
                return;

            CurrentLocation = Location.Land;
            CurrentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.KilometerPerHour, CurrentSpeed);
            CurrentSpeedUnit = SpeedUnit.KilometerPerHour;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }
        public void Takeoff()
        {
            if(!IsOnLand || IsStationary)
                return;
            if (ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.MeterPerSecond, CurrentSpeed) < s_minimumAllowedAirSpeed)
                return;

            CurrentLocation = Location.Air;
            CurrentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.MeterPerSecond, CurrentSpeed);
            CurrentSpeedUnit = SpeedUnit.MeterPerSecond;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }



        public override void Start()
        {
            if (IsInMotion || IsFlying)
                return;

            State = VehicleState.Moving;
            CurrentSpeed = 1;
        }
        public override void Stop()
        {
            if (IsStationary || IsFlying)
                return;

            State = VehicleState.Stationary;
            CurrentSpeed = 0;
        }
    }
}
