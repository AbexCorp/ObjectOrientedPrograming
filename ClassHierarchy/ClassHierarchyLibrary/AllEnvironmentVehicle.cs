using ClassHierarchyLibrary.ComboVehicleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public class AllEnvironmentVehicle : AmphibiousVehicle, ILandToAir, IWaterToAir
    {
        public void Landing()
        {
            throw new NotImplementedException();
        }

        public void Takeoff()
        {
            throw new NotImplementedException();
        }



        public void WaterLanding()
        {
            throw new NotImplementedException();
        }

        public void WaterTakeoff()
        {
            throw new NotImplementedException();
        }
    }
}
