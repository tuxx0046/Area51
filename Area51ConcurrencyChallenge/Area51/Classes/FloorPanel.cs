using System;
using System.Collections.Generic;
using System.Text;

namespace Area51.Classes
{
    public class FloorPanel
    {
        /// <summary>
        /// Check if person can go to target floor and if not handles it
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="person"></param>
        public void HandleRequest(Elevator elevator, IPerson person)
        {
            bool clearForAccess = VerifyAccessLevel(person);
            
            if (clearForAccess)
            {
                person.EnterElevator(elevator);
                InputFloorRequestToElevator(elevator, person.TargetFloor);
            }
            else
            {
                // TODO: Remove person completely or something due to no access clearance
                Console.WriteLine($"[Floor Panel]: {person.Id} does not have security clearance to floor {person.TargetFloor}.");
                elevator.MoveToNextFloorInQueue();
            }
        }

        /// <summary>
        /// Check if person has level access to target floor
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool VerifyAccessLevel(IPerson person)
        {
            if (person.SecurityCertificate < person.TargetFloor.FloorLevel)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Tells elevator to add requested floor on top of queue
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="floor"></param>
        public void InputFloorRequestToElevator(Elevator elevator, Floor floor)
        {
            elevator.AddToFirstInQueue(floor);
            Console.WriteLine("[Floor Panel]: Added request in top of elevator queue...");
        }
    }
}
