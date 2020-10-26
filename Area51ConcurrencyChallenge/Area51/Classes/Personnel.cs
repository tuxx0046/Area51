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
            Id = "Staff" + staffId.ToString();
            Console.WriteLine($"[Personnel]: {Id} with clearance level {SecurityCertificate} " +
                $"just spawned on {SpawnFloor.FloorName} and wants to go to {TargetFloor.FloorName}.");
        }

        public void CallElevator(Elevator elevator)
        {
            if (HasCalledElevator == false)
            {
                Console.WriteLine("[Personnel]: " + Id + " has pushed floor panel for elevator");
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
        }

        public void Die()
        {
            Console.WriteLine($"[Personnel]: The intruder {Id} has died");
            IsDead = true;
        }

    }
}
