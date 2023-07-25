using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ComboVehicleInterfaces
{
    public interface IWaterToAir
    {
        public int Buoyancy { get; }
        public void WaterTakeoff();
        public void WaterLanding();
    }
}
