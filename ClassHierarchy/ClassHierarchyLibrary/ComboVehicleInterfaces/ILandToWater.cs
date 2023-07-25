using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ComboVehicleInterfaces
{
    public interface ILandToWater
    {
        public int Buoyancy { get; }
        public int NumberOfWheels { get; }
        public void Sail();
        public void Dock();
    }
}
