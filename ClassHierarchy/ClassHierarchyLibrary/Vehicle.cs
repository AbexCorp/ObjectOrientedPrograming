using ClassHierarchyLibrary.ComboVehicleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public class Vehicle
    {
        protected static int s_minimumAllowedLandSpeed = 1; //km/h
        protected static int s_maximumAllowedLandSpeed = 350;
        protected static int s_minimumAllowedWaterSpeed = 1; //Knot
        protected static int s_maximumAllowedWaterSpeed = 40;
        protected static int s_minimumAllowedAirSpeed = 20; //m/s
        protected static int s_maximumAllowedAirSpeed = 200;


        public enum VehicleState { Stationary, Moving }
        public enum Location { Land, Water, Air }


        protected VehicleState _state;
        protected Location _location;
        protected Engine? _engine;
        protected int _currentSpeed;
        protected SpeedUnit _currentSpeedUnit;



        public VehicleState State { get { return _state; } protected set { _state = value; } }
        public Location CurrentLocation { get { return _location; } protected set { _location = value; } }
        public bool IsStationary { get { return State == VehicleState.Stationary ? true : false; } }
        public bool IsInMotion { get { return !IsStationary; } }
        public bool IsFlying { get { return CurrentLocation == Location.Air ? true : false; } }
        public bool IsInWater { get { return CurrentLocation == Location.Water ? true : false; } }
        public bool IsOnLand { get { return CurrentLocation == Location.Land ? true : false; } }



        

        public int CurrentSpeed { get { return _currentSpeed; } protected set { _currentSpeed = value; } }
        public SpeedUnit CurrentSpeedUnit { get { return _currentSpeedUnit; } protected set { _currentSpeedUnit = value; } }
        public virtual void Start()
        {
            if (IsInMotion)
                return;

            State = VehicleState.Moving;
            CurrentSpeed = 1;
        }
        public virtual void Stop()
        {
            if (IsStationary)
                return;

            State = VehicleState.Stationary;
            CurrentSpeed = 0;
        }
        public virtual void ChangeSpeed(int speedChange)
        {
            if (IsStationary)
                return;

            CurrentSpeed += speedChange;
            CurrentSpeed = SetCorrectSpeed(CurrentSpeed, CurrentSpeedUnit);
        }






        protected Engine? Engine { get { return _engine; } }
        public bool HasEngine { get { return Engine == null ? false : true; } }
        public Engine.EngineFuelType FuelType { get { return Engine.FuelType; } }
        public int Horsepower { get { return Engine.HorsePower; } }




        //ex: car, bike motorbike, scooter, amphibian, electric board, hovercraft
        //amphibia, boat, ship, submarine, motorboat, canoe, hovercraft
        //Plane, helicopter, glider, glider plane

        //Titan submarine
        //Nanban trade ship
        //hoverboard


        //Land: number of wheels, can be stoped any time. fluid transition into water movement
        //Water: buoyancy, with engine always powered by oil. can be stoped any time. fluid-
        //transition into land movement
        //Air: Can't stop in air, land and takeoff methods. goes into air with-
        //current land speed, above minimum air!! and vice-versa



        public enum SpeedUnit { KilometerPerHour, MeterPerSecond, Knots}
        public static int ChangeSpeedUnit(SpeedUnit currentUnit, SpeedUnit desiredUnit, double speed)
        {
            if(currentUnit == desiredUnit)
                return (int)Math.Round(speed, 0);

            if (currentUnit == SpeedUnit.MeterPerSecond)
                speed *= 3.6;
            if (currentUnit == SpeedUnit.Knots)
                speed *= 1.852;

            if (desiredUnit == SpeedUnit.KilometerPerHour)
                return (int)Math.Round(speed, 0);
            if (desiredUnit == SpeedUnit.MeterPerSecond)
                return (int)Math.Round(speed * 0.277777778, 0);
            if (desiredUnit == SpeedUnit.Knots)
                return (int)Math.Round(speed * 0.539956803, 0);
            return (int)Math.Round(speed, 0);
        }
        public int SetCorrectSpeed(int speed, SpeedUnit unit)
        {
            switch(unit)
            {
                case SpeedUnit.Knots:
                    if (speed < s_minimumAllowedWaterSpeed)
                        return s_minimumAllowedWaterSpeed;
                    if (speed > s_maximumAllowedWaterSpeed)
                        return s_maximumAllowedWaterSpeed;
                    return speed;


                case SpeedUnit.MeterPerSecond:
                    if (speed < s_minimumAllowedAirSpeed)
                        return s_minimumAllowedAirSpeed;
                    if (speed > s_maximumAllowedAirSpeed)
                        return s_maximumAllowedAirSpeed;
                    return speed;


                case SpeedUnit.KilometerPerHour:
                    if (speed < s_minimumAllowedLandSpeed)
                        return s_minimumAllowedLandSpeed;
                    if (speed > s_maximumAllowedLandSpeed)
                        return s_maximumAllowedLandSpeed;
                    return speed;
            }
            throw new ArgumentException("Unit not implemented");
        }
    }
}
