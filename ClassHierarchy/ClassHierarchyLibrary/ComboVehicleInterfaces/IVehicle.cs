using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClassHierarchyLibrary.Vehicle;

namespace ClassHierarchyLibrary.ComboVehicleInterfaces
{
    public interface IVehicle
    {
        //This is useles because protected set still needs to be public?
        public VehicleState State { get; protected set; }
        public Location CurrentLocation { get; protected set; }
        public int CurrentSpeed { get; protected set; }
        public SpeedUnit CurrentSpeedUnit { get; protected set; }
    }
}
