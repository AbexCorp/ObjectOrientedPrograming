﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary
{
    public abstract class WaterVehicle : Vehicle
    {
        protected int _buoyancy;
        public int Buoyancy { get { return _buoyancy; } }
    }
}
