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
                $"just spawned on {(SpawnFloor.floorName == "Ground" ? "Ground Floor" : "Floor " + SpawnFloor.floorName)} " +
                $"and wants to go to {(TargetFloor.floorName == "Ground" ? "Ground Floor" : "Floor " + TargetFloor.floorName)}.");
        }

        public void CallElevator(Elevator elevator)
        {
            if (HasCalledElevator == false)
            {
                Console.WriteLine("[Personnel]: " + Id + " has pressed button and called elevator to " + SpawnFloor.floorName);
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
        /// Sets IPerson IsDead status to true. Used for killconfirm.
        /// </summary>
        public void Die()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[Personnel]: The intruder {Id} has died");
            Console.ResetColor();
            IsDead = true;
        }

    }
}
