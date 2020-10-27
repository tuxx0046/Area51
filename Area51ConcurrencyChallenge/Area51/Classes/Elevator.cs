﻿using System;
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

        /// <summary>
        /// Goes to next floor in queue.
        /// </summary>
        /// <returns>Floor that the elevator moved to (the next in queue)</returns>
        public Floor MoveToNextFloorInQueue()
        {
            if (_queue.Count > 0)
            {
                if (CurrentFloor != null)
                {
                    CurrentFloor.UnCallElevator();
                }
                TargetFloor = _queue[0];
                Console.WriteLine("[Elevator]: elevator is moving to " + TargetFloor.FloorName + "...");
                // TODO : time has to pass
                ArrivedAtFloor();
                _queue.RemoveAt(0);
                return CurrentFloor;
            }
            else
            {
                Console.WriteLine("[Elevator]: No floors in queue");
                return null;
            }
        }

        /// <summary>
        /// Remove person in elevator
        /// </summary>
        public void ExitPersonInElevator()
        {
            Console.WriteLine("[Elevator]: " + personInElevator.Id + " has exited elevator and reached target floor");
            // TODO: personInElevator garbage collect
            personInElevator = null;
        }

        /// <summary>
        /// Remove call floor panel call from elevator's queue
        /// </summary>
        public void ArrivedAtFloor()
        {
            Console.WriteLine("[Elevator]: Arrived at {0}", TargetFloor.FloorName == "Ground" ? "Ground Floor" : "Floor " + TargetFloor.FloorName);
            CurrentFloor = TargetFloor;
            
            if (personInElevator != null)
            {
                ExitPersonInElevator();
            }
            
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
            if (_queue.Count != 0)
            {

                Console.WriteLine("[Elevator]: Queue count: " + _queue.Count);
                for (int i = 0; i < _queue.Count; i++)
                {
                    Console.WriteLine("[Elevator]: Number {0} in queue: {1}", i + 1, _queue[i].FloorName == "Ground" ? "Ground Floor" : "Floor " + _queue[i].FloorName);
                }
            }
            else
            {
                Console.WriteLine("[Elevator]: No floors calls in queue");
            }

        }
    }
}