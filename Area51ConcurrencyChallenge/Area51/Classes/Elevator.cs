using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51.Classes
{
    public class Elevator
    {
        public readonly FloorPanel floorPanel = new FloorPanel();
        public bool isOccupied = false;
        public IPerson personInElevator;
        public Floor CurrentFloor { get; set; }
        public int TargetFloor { get; set; } = 0;
        private List<Floor> _queue = new List<Floor>();

        public void CarryPersonToTargetFloor(IPerson person)
        {
            TargetFloor = person.TargetFloor;
            // TODO: after some time passes
            TargetFloor = -1;
            // TODO: person has reached goal - remove person completely
        }

        public void MoveToNextFloorInQueue()
        {
            CurrentFloor = _queue[0];
            // TODO : time has to pass
            ArrivedToFloor();
            Console.WriteLine("Elevator has arrived at {0}", CurrentFloor.FloorName == "Ground" ? "Ground Floor" : "Floor " + CurrentFloor.FloorName);
            _queue.RemoveAt(0);
        }

        /// <summary>
        /// Remove call floor panel call from elevator's queue
        /// </summary>
        public void ArrivedToFloor()
        {
            CurrentFloor.UnCallElevator();
        }

        /// <summary>
        /// Add floor panel call to elevator's queue
        /// </summary>
        /// <param name="floor"></param>
        public void AddCallToQueue(Floor floor)
        {
            _queue.Add(floor);
        }

        /// <summary>
        /// Print elevator's queue to console
        /// </summary>
        public void ShowElevatorQueue()
        {
            Console.WriteLine("Elevator queue: ");
            for (int i = 0; i < _queue.Count; i++)
            {
                Console.WriteLine($"Number {i + 1} in queue: Floor {_queue[i]}");
            }
        }
    }
}
