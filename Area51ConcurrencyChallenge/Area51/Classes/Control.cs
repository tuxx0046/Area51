using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Control
    {
        private readonly Elevator _elevator;
        private readonly Floor _defaultFloor;
        private Floor elevatorsCurrentFloor;
        public FloorPanel floorPanel = new FloorPanel();

        public Control(Elevator elevator, Floor defaultFloor)
        {
            _elevator = elevator;
            _defaultFloor = defaultFloor;
        }

        public Floor MoveElevator()
        {
            elevatorsCurrentFloor = _elevator.MoveToNextFloorInQueue();
            return elevatorsCurrentFloor;
        }

        /// <summary>
        /// Get scan results from floor scanner and checks security certificate of the person who called the elevator.
        /// <br>Depending on check, control will send commands to turret or elevator.</br>
        /// </summary>
        /// <param name="floor">Should be the floor that elevator was called to</param>
        public void RetrieveScanResult(Floor floor)
        {
            IPerson person = floor._scanner.SendScanResult();
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
                bool killConfirmed = person.SpawnFloor._turret.ConfirmKill(person);
                if (killConfirmed)
                {
                    person.SpawnFloor.RemovePersonFromFloor(person);
                }
                // TODO: move elevator to next in queue
            }
            else
            {
                // Staff
                Console.WriteLine($"[Control]: {person.Id} is a verified staff member of AREA 51.");
                bool requestAccepted = floorPanel.HandleRequest(_elevator, person);
                // If request not accepted
                if (requestAccepted == false)
                {
                    RedirectNonClearedPersonnel(person);
                }

                elevatorsCurrentFloor = _elevator.MoveToNextFloorInQueue();
            }
        }

        /// <summary>
        /// Redirect personnel with clearance problems
        /// </summary>
        /// <param name="person"></param>
        private void RedirectNonClearedPersonnel(IPerson person)
        {
            // Spawned on floor with no clearance - reroute to removal
            if (person.SecurityCertificate < person.SpawnFloor.FloorLevel)
            {
                Console.WriteLine($"[Control]: No security clearance to current floor. Security called to escort {person.Id} to tortu... interrogation facility.");
                _elevator.ExitPersonInElevator();
                _elevator.CurrentFloor.RemovePersonFromFloor(person);
            }
            // Wants access to floor without clearance - reroute to upper most floor
            else
            {
                // Don't move elevator if spawnfloor is default floor
                if (person.SpawnFloor == _defaultFloor)
                {
                    Console.WriteLine($"[Control]: {person.Id} has no clearance to requested floor. Elevator won't move until {person.Id} leaves elevator");
                    _elevator.ExitPersonInElevator();
                    person.SpawnFloor.RemovePersonFromFloor(person);
                }
                // Spawnfloor is different from default floor
                else
                {
                    Console.WriteLine($"[Control]: No security clearance to requested floor. Sending elevator to default floor. {person.Id} is advised to contact the administration's office.");
                    _elevator.AddToFirstInQueue(_defaultFloor);
                }
            }
        }
    }
}
