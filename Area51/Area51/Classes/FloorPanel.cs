using System;
using System.Collections.Generic;
using System.Text;

namespace Area51.Classes
{
    public class FloorPanel
    {
        /// <summary>
        /// Handles if person can go to target floor.
        /// <br>Returns true if IPerson has sufficient access clearance.</br>
        /// <br>Returns false if IPerson has insufficient access.</br>
        /// </summary>
        /// <param name="elevator"></param>
        /// <param name="person"></param>
        public bool HandleRequest(Elevator elevator, IPerson person)
        {
            bool clearForAccess = VerifyAccessLevel(person);
            if (clearForAccess)
            {
                Console.WriteLine($"[Floor Panel]: {person.Id} is cleared for access to requested floor.");
                InputFloorRequestToElevator(elevator, person.TargetFloor);
                return true;
            }
            else
            {
                Console.WriteLine($"[Floor Panel]: {person.Id} does not have security clearance to {person.TargetFloor.floorName}. Request not accepted.");
                return false;
            }
        }

        /// <summary>
        /// Check if person has level access to target floor
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        private bool VerifyAccessLevel(IPerson person)
        {
            Console.WriteLine($"[Floor Panel]: Checking clearance level...");
            if (person.SecurityCertificate < person.TargetFloor.SecurityLevel)
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
        private void InputFloorRequestToElevator(Elevator elevator, Floor floor)
        {
            Console.WriteLine("[Floor Panel]: Add request to top of elevator queue...");
            elevator.AddFloorToTopOfQueue(floor);
        }
    }
}
