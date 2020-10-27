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
            Control control = new Control(elevator, floors[0]);

            //SpawnPersonOnRandomFloor(1);
            //SpawnPersonOnRandomFloor(2);

            // Create personnel with clearance
            //Personnel pPerson = new Personnel(numberOfClearanceLevels, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(person);

            // Create personnel with insufficient clearance to target floor, and spawned floor is default floor
            //Personnel person = new Personnel(1, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(person);

            // Create personnel with insufficient clearance to target floor, and spawned floor is not default floor
            //Personnel person = new Personnel(2, floors[3], floors[1], 0);
            //floors[1].Personnel.Add(person);

            // Create personnel with insufficient clearance to spawned floor
            //Personnel person = new Personnel(1, floors[3], floors[2], 0);
            //floors[2].Personnel.Add(person);

            // Create intruder on floor 0
            Personnel spawnedPerson = new Personnel(0, floors[3], floors[0], 0);
            floors[0].Personnel.Add(spawnedPerson);

            // Check all floors for new spawn and let it call elevator
            for (int i = 0; i < floors.Count; i++)
            {
                // A floor can only call elevator once (pushing call button multiple times won't stack in queue)
                // Only if there's a person on the floor the person can push the floor's call button
                // TODO: Maybe the one floor one call rule should be removed and add a called field to personnel instead?  If so scanner has to be reworked
                if (floors[i].CalledElevator == false && floors[i].Personnel.Count != 0)
                {
                    // First person in line gets to call elevator
                    IPerson person = floors[i].Personnel[0];
                    person.CallElevator(elevator);
                    floors[i]._scanner.ScanPerson(person);
                }
            }

            elevator.ShowElevatorQueue();
            // Elevator moves
            Floor currentFloorOfElevator = control.MoveElevator();
            // Person enters
            if (currentFloorOfElevator.Personnel[0] == spawnedPerson)
            {
                if (spawnedPerson.HasCalledElevator)
                {
                    if (spawnedPerson.SpawnFloor == elevator.CurrentFloor)
                    {
                        spawnedPerson.EnterElevator(elevator);
                    }
                }
            }
            // control checks clearance level of person in elevator
            control.RetrieveScanResult(currentFloorOfElevator);


            // When arrived to floor let control decide what to do

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
