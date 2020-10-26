using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Control
    {
        private  Elevator _elevator = new Elevator();

        public List<Floor> floors { get; set; }

        public void InputFloor(Floor floor)
        {
            floors.Add(floor);
        }

        public void HandleElevatorRequest(IPerson person)
        {
            bool acceptElevatorMoveRequest = CheckCertificate(person);
            if (acceptElevatorMoveRequest)
            {
                AllowElevatorMove(person);
            }
        }

        public bool CheckCertificate(IPerson person)
        {
            if (person.SecurityCertificate == 0)
            {
                Console.WriteLine($"{person.Id} has clearance level {person.SecurityCertificate} and is an intruder. Elevator floor panel disabled");
                person.MarkedForTermination = true;
                person.SpawnFloor.RelayKillOrder(person);
                return false;
            }
            else if (person.SecurityCertificate > person.TargetFloor.FloorLevel)
            {
                Console.WriteLine($"{person.Id} does not have security clearance to floor {person.TargetFloor}.");
                // TODO: remove personnel with not clearance
                return false;
            }
            // Cleared
            return true;
        }

        public void AllowElevatorMove(IPerson person)
        {
            //_elevator.MoveToFloor(person);
        }

        
    }
}
