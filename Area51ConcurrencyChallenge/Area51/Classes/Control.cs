using Area51.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Control
    {
        private Elevator _elevator;
        public FloorPanel floorPanel = new FloorPanel();
        public List<Floor> floors { get; set; }

        public void InputFloor(Floor floor)
        {
            floors.Add(floor);
        }

        public Control(Elevator elevator)
        {
            _elevator = elevator;
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
                // TODO: move elevator to next in queue
            }
            else
            {
                // Staff
                bool requestAccepted = floorPanel.HandleRequest(_elevator, person);
                // If request not accepted
                if (requestAccepted == false)
                {
                    RerouteNonClearedPersonnel(person);
                }
            }
        }

        private void RerouteNonClearedPersonnel(IPerson person)
        {
            // Spawned on floor with no clearance - reroute to removal
            if (person.SecurityCertificate < person.SpawnFloor.FloorLevel)
            {
                Console.WriteLine($"[Control]: No security clearance to requested floor. Security called to escort {person.Id} to tortu... interrogation facility.");
                _elevator.CurrentFloor.Personnel.Remove(person);
                person.Die();
            }
            // Wants access to floor without clearance - reroute to upper most floor
            else
            {

            }
                
        }
    }
}
