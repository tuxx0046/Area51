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
        //static Dictionary<int, Floor> floors = new Dictionary<int, Floor>();
        static List<Floor> floors = new List<Floor>();
        static int numberOfClearanceLevels;

        static void Main(string[] args)
        {
            AddFloors();
            numberOfClearanceLevels = floors.Count;
            Elevator elevator = new Elevator();

            SpawnPersonOnRandomFloor(1);
            SpawnPersonOnRandomFloor(2);

            // Check all floors for new spawns and let them call elevator

            for (int i = 0; i < floors.Count; i++)
            {
                Console.WriteLine(floors[i].Personnel.Count); 
                if (floors[i].CalledElevator == false && floors[i].Personnel.Count != 0)
                {
                    // First person in line gets to call elevator
                    floors[i].Personnel[0].CallElevator(elevator);
                }
            }

            elevator.ShowElevatorQueue();
            elevator.MoveToNextFloorInQueue();
            elevator.ShowElevatorQueue();
            // floors er en liste af floor og ikke string name of floor

            //elevator.CarryPersonToTargetFloor();


        }

        public static void AddFloors()
        {
            floors.Add(Factory.CreateFloor(0, "Ground"));
            floors.Add(Factory.CreateFloor(1, "B1"));
            floors.Add(Factory.CreateFloor(2, "B2"));
            floors.Add(Factory.CreateFloor(3, "B3"));
        }

        public static void SpawnPersonOnRandomFloor(int id)
        {
            Random rnd = new Random();
            int randomFloor;
            if (floors.Count > 0)
            {
                randomFloor = rnd.Next(0, floors.Count);
                floors[randomFloor].SpawnNewPerson(numberOfClearanceLevels, floors[randomFloor], floors, id);
            }
        }
    }
}
