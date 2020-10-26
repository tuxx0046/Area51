using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Area51
{
    public class Floor
    {
        private readonly IPanel _panel;
        private readonly IScanner _scanner;
        private readonly ITurret _turret;
        public readonly string FloorName;
        public bool CalledElevator { get; private set; } = false;
        public List<IPerson> Personnel = new List<IPerson>();
        public int FloorLevel { get; }

        public Floor(IPanel panel, IScanner scanner, ITurret turret, int floorLevel, string floorName)
        {
            _panel = panel;
            _scanner = scanner;
            _turret = turret;
            FloorLevel = floorLevel;
            FloorName = floorName;
        }

        public void RelayKillOrder(IPerson person)
        {
            _turret.EliminateTarget(person, FloorName);
        }

        public IPerson SendScanResult()
        {
            return _scanner.SendScanResult();
        }

        public void SpawnNewPerson(int numberOfClearanceLevels, Dictionary<int, Floor> floors, int id)
        {
            Personnel.Add(Factory.CreatePerson(numberOfClearanceLevels, floors, id));
        }

        public void CallElevator(Elevator elevator)
        {
            if (CalledElevator == false)
            {
                Console.WriteLine("[Floor]: " + FloorName + " has called Elevator");
                CalledElevator = true;
                elevator.AddCallToQueue(this);
            }
            else
            {
                Console.WriteLine("[Floor]: " + FloorName + " has already called elevator");
            }
        }

        public void UnCallElevator()
        {
            CalledElevator = false;
        }
    }
}
