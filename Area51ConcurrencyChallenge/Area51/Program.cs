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
            Control control = new Control(elevator);

            //SpawnPersonOnRandomFloor(1);
            //SpawnPersonOnRandomFloor(2);

            // Create intruder on floor 0
            //Personnel intruder = new Personnel(0, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(intruder);

            // Create personnel with insufficient clearance
            Personnel notClearedPerson = new Personnel(1, floors[3], floors[0], 0);
            floors[0].Personnel.Add(notClearedPerson);

            // Check all floors for new spawn and let it call elevator
            for (int i = 0; i < floors.Count; i++)
            {
                // A floor can only call elevator once (pushing call button multiple times won't stack in queue)
                // Only if there's a person on the floor the person can push the floor's call button
                // TODO: Maybe the one floor one call rule should be removed and add a called field to personnel instead?
                if (floors[i].CalledElevator == false && floors[i].Personnel.Count != 0)
                {
                    // First person in line gets to call elevator
                    IPerson person = floors[i].Personnel[0];
                    person.CallElevator(elevator);
                    floors[i]._scanner.ScanPerson(person);
                }
            }

            elevator.ShowElevatorQueue();
            Floor currentFloorOfElevator = elevator.MoveToNextFloorInQueue();
            if (currentFloorOfElevator.Personnel[0] == notClearedPerson)
            {
                if (notClearedPerson.HasCalledElevator)
                {
                    if (notClearedPerson.SpawnFloor == elevator.CurrentFloor)
                    {
                        notClearedPerson.EnterElevator(elevator);
                    }
                }
            }
            // When arrived to floor let control decide what to do
            control.RetrieveScanResult(currentFloorOfElevator);

            // Check to see if there's any person left
            for (int i = 0; i < floors.Count; i++)
            {
                // A floor can only call elevator once (pushing call button multiple times won't stack in queue)
                // Only if there's a person on the floor the person can push the floor's call button
                if (floors[i].Personnel.Count != 0)
                {
                    Console.WriteLine(floors[i].Personnel[0].Id);
                }
                else
                {
                    Console.WriteLine($"[{floors[i].FloorName}]: No one on floor");
                }
            }
        }

        public static void AddFloors()
        {
            floors.Add(Factory.CreateFloor(1, "Ground"));
            floors.Add(Factory.CreateFloor(2, "B1"));
            floors.Add(Factory.CreateFloor(3, "B2"));
            floors.Add(Factory.CreateFloor(4, "B3"));
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
