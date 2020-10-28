using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Area51
{
    public class Floor
    {
        public readonly IScanner scanner;
        public readonly ITurret turret;
        public readonly string floorName;
        public bool CalledElevator { get; private set; } = false;
        public List<IPerson> Personnel = new List<IPerson>();
        public int SecurityLevel { get; }

        public Floor(IScanner scanner, ITurret turret, int floorLevel, string floorName)
        {
            this.scanner = scanner;
            this.turret = turret;
            SecurityLevel = floorLevel;
            this.floorName = floorName;
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
                Console.WriteLine($"[Floor]: {floorName} is relaying target({person.Id}) to turret...");
                turret.EliminateTarget(person, floorName);
            }
            // This should never happen!
            else
            {
                Console.WriteLine("[Floor]: Target " + person.Id + " is no longer on " + this.floorName + "!");
            }
        }

        public IPerson SpawnNewPerson(int numberOfClearanceLevels, Floor spawnFloor, List<Floor> floors, int id)
        {
            IPerson person = Factory.CreatePerson(numberOfClearanceLevels, spawnFloor, floors, id);
            Personnel.Add(person);
            return person;
        }

        public void CallElevator(Elevator elevator)
        {
            if (CalledElevator == false)
            {
                Console.WriteLine("[Floor]: " + floorName + " has called Elevator");
                CalledElevator = true;
                elevator.AddCallToQueue(this);
            }
            else
            {
                Console.WriteLine("[Floor]: " + floorName + " has already called elevator");
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
                Console.WriteLine($"[Floor]: {person.Id} has left {(floorName == "Ground" ? floorName + " Floor" : "Floor " + floorName)}.");
            }
        }
    }
}
