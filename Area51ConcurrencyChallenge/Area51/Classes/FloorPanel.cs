﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Area51.Classes
{
    public class FloorPanel
    {
        /// <summary>
        /// Check if person can go to target floor and if not handles it
        /// <br>by sending person to must upper floor</br>
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
                Console.WriteLine($"[Floor Panel]: {person.Id} does not have security clearance to {person.TargetFloor.FloorName}. Request not accepted.");
                return false;
            }
        }

        /// <summary>
        /// Check if person has level access to target floor
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool VerifyAccessLevel(IPerson person)
        {
            Console.WriteLine($"[Floor Panel]: Checking clearance level...");
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
            Console.WriteLine("[Floor Panel]: Add request to top of elevator queue...");
            elevator.AddToFirstInQueue(floor);
        }
    }
}
