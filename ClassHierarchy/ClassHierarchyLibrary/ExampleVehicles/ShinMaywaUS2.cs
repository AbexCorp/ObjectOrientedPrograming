using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ExampleVehicles
{
    public class ShinMaywaUS2 : AllEnvironmentVehicle
    {
        public ShinMaywaUS2(int buoyancy, Engine.EngineFuelType fuelType) : base(buoyancy: buoyancy, numberOfWheels: 4, engine: new Engine(50000, fuelType)) { }
    }
}
