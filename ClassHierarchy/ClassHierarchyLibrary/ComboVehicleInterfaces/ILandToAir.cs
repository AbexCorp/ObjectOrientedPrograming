using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ComboVehicleInterfaces
{
    public interface ILandToAir
    {
        public int NumberOfWheels { get; }
        public void Takeoff();
        public void Landing();
    }
}
