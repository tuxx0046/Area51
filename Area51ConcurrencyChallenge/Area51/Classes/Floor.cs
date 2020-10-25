using Area51.Classes;
using System;
using System.Collections.Generic;
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

        public Floor(IPanel panel, IScanner scanner, ITurret turret, string floorName)
        {
            _panel = panel;
            _scanner = scanner;
            _turret = turret;
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

        public void SpawnNewPerson(int numberOfClearanceLevels, int numberOfFloors, int id)
        {
            Personnel.Add(Factory.CreatePerson(numberOfClearanceLevels, numberOfFloors, id));
        }

        public void CallElevator(Elevator elevator)
        {
            CalledElevator = true;
            elevator.AddCallToQueue(this);
        }

        public void UnCallElevator()
        {
            CalledElevator = false;
        }
    }
}
