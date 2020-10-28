using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Control
    {
        public readonly Elevator elevator;
        public readonly Floor defaultFloor;
        public Floor elevatorsCurrentFloor;
        public FloorPanel floorPanel = new FloorPanel();

        public Control(Elevator elevator, Floor defaultFloor)
        {
            this.elevator = elevator;
            this.defaultFloor = defaultFloor;
        }

        /// <summary>
        /// Returns the floor that the elevator has moved to.
        /// </summary>
        /// <returns></returns>
        public Floor MoveElevator()
        {
            elevatorsCurrentFloor = elevator.MoveToNextFloorInQueue();
            return elevatorsCurrentFloor;
        }

        /// <summary>
        /// Get scan results from floor scanner and checks security certificate of the person who called the elevator.
        /// <br>Depending on check, control will send commands to turret or elevator.</br>
        /// </summary>
        /// <param name="floor">Should be the floor that elevator was called to</param>
        public void RetrieveScanResult(Floor floor)
        {
            IPerson person = floor.scanner.SendScanResult();
            CheckCertificate(person);
        }

        public void CheckCertificate(IPerson person)
        {
            if (person.SecurityCertificate == 0)
            {
                // Intruder
                Console.WriteLine($"[Control]: {person.Id} has clearance level {person.SecurityCertificate} and is an intruder.");
                person.MarkedForTermination = true;
                Console.WriteLine("[Control]: Giving kill order to turret. Target is {0}", person.Id);
                person.SpawnFloor.RelayKillOrder(person);
                // Wait for kill confirm, because if person is in elevator it can't just go to next floor
                bool killConfirmed = person.SpawnFloor.turret.ConfirmKill(person);
                if (killConfirmed)
                {
                    Console.WriteLine($"[Control]: Kill confirmation received. Dispatching cleanup team.");
                }
                // Shouldn't happen
                else
                {
                    Console.WriteLine("[Control]: Kill confirm not recieved!");
                }
                elevator.MoveToNextFloorInQueue();
            }
            else
            {
                // Staff
                Console.WriteLine($"[Control]: {person.Id} is a verified staff member of AREA 51.");
                bool requestAccepted = floorPanel.HandleRequest(elevator, person);
                // If request not accepted
                if (requestAccepted == false)
                {
                    RedirectNonClearedPersonnel(person);
                }

                elevatorsCurrentFloor = elevator.MoveToNextFloorInQueue();
            }
        }

        /// <summary>
        /// Redirect personnel with clearance problems
        /// </summary>
        /// <param name="person"></param>
        private void RedirectNonClearedPersonnel(IPerson person)
        {
            // Spawned on floor with no clearance - reroute to removal
            if (person.SecurityCertificate < person.SpawnFloor.SecurityLevel)
            {
                Console.WriteLine($"[Control]: Also no security clearance to current floor. Security called to escort {person.Id} to tortu... interrogation facility.");
                elevator.ExitPersonInElevator();
                elevator.CurrentFloor.RemovePersonFromFloor(person);
            }
            // Wants access to floor without clearance - reroute to upper most floor
            else
            {
                // Don't move elevator if spawnfloor is default floor
                if (person.SpawnFloor == defaultFloor)
                {
                    Console.WriteLine($"[Control]: {person.Id} has no clearance to requested floor. Elevator won't move until {person.Id} leaves elevator");
                    elevator.ExitPersonInElevator();
                    person.SpawnFloor.RemovePersonFromFloor(person);
                }
                // Spawnfloor is different from default floor
                else
                {
                    Console.WriteLine($"[Control]: No security clearance to requested floor. Sending elevator to default floor. {person.Id} is advised to contact the administration's office.");
                    elevator.AddFloorToTopOfQueue(defaultFloor);
                }
            }
        }
    }
}
