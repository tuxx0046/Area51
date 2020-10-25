using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using Area51.Classes;
namespace Area51
{
    class Program
    {
        static Dictionary<int, Floor> floors = new Dictionary<int, Floor>();
        static int numberOfClearanceLevels;

        static void Main(string[] args)
        {
            AddFloors();
            numberOfClearanceLevels = floors.Count;
            Elevator elevator = new Elevator();

            SpawnPersonOnRandomFloor(1);
            for (int i = 0; i < floors.Count; i++)
            {
                if (floors[i].CalledElevator == false && floors[i].Personnel.Count != 0)
                {
                    floors[i].CallElevator(elevator);
                }
            }
            elevator.ShowElevatorQueue();
            elevator.MoveToNextFloorInQueue();
            elevator.ShowElevatorQueue();
            // floors er en liste af floor og ikke string name of floor
            
            elevator.CarryPersonToTargetFloor();

        }

        public static void AddFloors()
        {
            floors.Add(0, Factory.CreateFloor("Ground"));
            floors.Add(1, Factory.CreateFloor("B1"));
            floors.Add(2, Factory.CreateFloor("B2"));
            floors.Add(3, Factory.CreateFloor("B3"));
        }

        public static void SpawnPersonOnRandomFloor(int id)
        {
            Random rnd = new Random();
            int randomFloor;
            if (floors.Count > 0)
            {
                randomFloor = rnd.Next(0, floors.Count);
                floors[randomFloor].SpawnNewPerson(numberOfClearanceLevels, floors.Count, id);
            }
        }
    }
}
