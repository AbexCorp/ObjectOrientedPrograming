using ClassHierarchyLibrary.ComboVehicleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public class SeaPlane : WaterVehicle, IWaterToAir
    {
        public void WaterLanding()
        {
            if (!IsFlying || IsStationary)
                return;
            if (ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.Knots, CurrentSpeed) > s_maximumAllowedWaterSpeed)
                return;

            CurrentLocation = Location.Water;
            CurrentSpeed = ChangeSpeedUnit(CurrentSpeedUnit, SpeedUnit.Knots, CurrentSpeed);
            CurrentSpeedUnit = SpeedUnit.Knots;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }
        public void WaterTakeoff()
        {
            if (!IsInWater || IsStationary)
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
