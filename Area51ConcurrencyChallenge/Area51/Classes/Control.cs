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
                floorPanel.HandleRequest(_elevator, person);
            }
        }
    }
}
