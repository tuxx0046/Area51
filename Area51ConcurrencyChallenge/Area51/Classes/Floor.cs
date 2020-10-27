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
        public readonly IScanner _scanner;
        public readonly ITurret _turret;
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

        /// <summary>
        /// Sends kill order to turret.
        /// <br>Removes person from Personnel list because it's an intruder!</br>
        /// </summary>
        /// <param name="person"></param>
        public void RelayKillOrder(IPerson person)
        {
            // Check to make sure that calling person matches floor's personnel before acting
            if (person == Personnel[0])
            {
                Console.WriteLine($"[Floor]: {FloorName} is relaying target({person.Id}) to turret...");
                _turret.EliminateTarget(person, FloorName);
            }
            // This should never happen!
            else
            {
                Console.WriteLine("[Floor]: Target " + person.Id + " is no longer on " + this.FloorName + "!");
            }
        }

        public IPerson SendScanResult()
        {
            return _scanner.SendScanResult();
        }

        public void SpawnNewPerson(int numberOfClearanceLevels, Floor spawnFloor, List<Floor> floors, int id)
        {
            IPerson person = Factory.CreatePerson(numberOfClearanceLevels, spawnFloor, floors, id);
            Personnel.Add(person);
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

        public void RemovePersonFromFloor(IPerson person)
        {
            if (person == Personnel[0])
            {
                Personnel.Remove(person);
                Console.WriteLine($"[Floor]: {person.Id} is no longer on {(FloorName == "Ground" ? FloorName + " Floor" : "Floor " + FloorName)}.");
            }
        }
    }
}
