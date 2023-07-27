using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ExampleVehicles
{
    public class Bicycle : LandVehicle
    {
        public Bicycle() : base(numberOfWheels: 2, engine: null) { }
    }
}
