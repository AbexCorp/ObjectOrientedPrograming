using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassHierarchyLibrary.ExampleVehicles
{
    public class NanbanTradeShip : WaterVehicle
    {
        private int _gunners;
        private int _essentialCrew;
        private int _numberOfCannons;

        public int Gunners { get { return _gunners; } }
        public int EssentialCrew { get {  return _essentialCrew; } }
        public int TotalCrew { get { return Gunners + EssentialCrew; } }
        public int Cannons { get { return _numberOfCannons; } }
        
        public NanbanTradeShip(int buoyancy, int horsepower, int gunners, int essentialCrew, int numberOfCannons)
            : base(buoyancy: buoyancy, new Engine(horsepower, Engine.EngineFuelType.Oil))
        {
            _gunners = gunners;
            _essentialCrew = essentialCrew;
            _numberOfCannons = numberOfCannons;
        }
    }
}
