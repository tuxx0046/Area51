using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51.Classes
{
    public class Elevator
    {
        public readonly FloorPanel floorPanel = new FloorPanel();
        public IPerson personInElevator;
        public Floor CurrentFloor { get; set; }
        public Floor TargetFloor { get; set; } = null;
        private List<Floor> _queue = new List<Floor>();

        public void CarryPersonToTargetFloor(IPerson person)
        {
            TargetFloor = person.TargetFloor;
            // TODO: after some time passes reset elevator target floor
            // TODO: person has reached goal - remove person completely
        }

        public void MoveToNextFloorInQueue()
        {
            if (_queue.Count > 0)
            {
                CurrentFloor.UnCallElevator();
                TargetFloor = _queue[0];
                // TODO : time has to pass
                ArrivedAtFloor();
                Console.WriteLine("[Elevator]: Arrived at {0}", TargetFloor.FloorName == "Ground" ? "Ground Floor" : "Floor " + TargetFloor.FloorName);
                _queue.RemoveAt(0);
            }
            else
            {
                Console.WriteLine("[Elevator]: No floors in queue");
            }
        }

        public void ExitPersonInElevator()
        {
            // TODO: personInElevator garbage collect
            personInElevator = null;
        }

        /// <summary>
        /// Remove call floor panel call from elevator's queue
        /// </summary>
        public void ArrivedAtFloor()
        {
            CurrentFloor = TargetFloor;
            ExitPersonInElevator();
            TargetFloor = null;
        }

        /// <summary>
        /// Add floor panel call to elevator's queue
        /// </summary>
        /// <param name="floor"></param>
        public void AddCallToQueue(Floor floor)
        {
            _queue.Add(floor);
            Console.WriteLine("[Elevator]: " + floor.FloorName + " added to queue");
        }

        public void AddToFirstInQueue(Floor floor)
        {
            _queue.Insert(0, floor);
            Console.WriteLine("[Elevator]: " + floor.FloorName + " added to first in queue");

        }


        /// <summary>
        /// Print elevator's queue to console
        /// </summary>
        public void ShowElevatorQueue()
        {
            Console.WriteLine("[Elevator]: Queue count: " + _queue.Count);
            Console.WriteLine("[Elevator]: queue: ");
            for (int i = 0; i < _queue.Count; i++)
            {
                Console.WriteLine("[Elevator]: Number {0} in queue: {1}", i + 1, _queue[i].FloorName == "Ground" ? "Ground Floor" : "Floor " + _queue[i].FloorName);
            }
        }
    }
}
