using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Personnel : IPerson
    {
        public string Id { get; }
        public int SecurityCertificate { get; }
        public Floor TargetFloor { get; }
        public Floor SpawnFloor { get; }
        public bool IsDead { get; set; } = false;
        public bool MarkedForTermination { get; set; } = false;
        public bool HasCalledElevator { get; set; } = false;

        public Personnel(int clearanceLevel, Floor targetFloor, Floor spawnFloor, int staffId)
        {
            SecurityCertificate = clearanceLevel;
            TargetFloor = targetFloor;
            SpawnFloor = spawnFloor;
            Id = clearanceLevel > 0 ? "Staff" + staffId.ToString() : "Staff" + staffId.ToString() + "(intruder)";
            Console.WriteLine($"[Personnel]: {Id} with clearance level {SecurityCertificate} " +
                $"just spawned on {(SpawnFloor.FloorName == "Ground" ? "Ground Floor" : "Floor " + SpawnFloor.FloorName)} " +
                $"and wants to go to {(TargetFloor.FloorName == "Ground" ? "Ground Floor" : "Floor " + TargetFloor.FloorName)}.");
        }

        public void CallElevator(Elevator elevator)
        {
            if (HasCalledElevator == false)
            {
                Console.WriteLine("[Personnel]: " + Id + " has pressed button and called elevator to " + SpawnFloor.FloorName);
                SpawnFloor.CallElevator(elevator);
                HasCalledElevator = true;
            }
            else
            {
                Console.WriteLine("[Personnel]: " + Id + " has already called elevator");
            }
            
        }

        public void EnterElevator(Elevator elevator)
        {
            elevator.personInElevator = this;
            Console.WriteLine($"[Personnel]: {Id} has entered elevator");
        }

        public void LeaveFloor()
        {
            SpawnFloor.RemovePersonFromFloor(this);
        }

        /// <summary>
        /// Removes person from spawned floor Personnel list.
        /// <br>Dies and cleans itself up</br>
        /// </summary>
        public void Die()
        {
            Console.WriteLine($"[Personnel]: The intruder {Id} has died");
            IsDead = true;
        }

    }
}
