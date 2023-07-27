using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ExampleVehicles
{
    public class Van : LandVehicle
    {
        public Van(int horsepower, Engine.EngineFuelType fuelType) : base(numberOfWheels: 4, new Engine(horsepower, fuelType))
        {

        }
    }
}
