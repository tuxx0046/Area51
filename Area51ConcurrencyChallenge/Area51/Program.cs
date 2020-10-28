using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Area51.Classes;
namespace Area51
{
    class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            List<Floor> floors = new List<Floor>();
            List<IPerson> personnel = new List<IPerson>();
            int numberOfClearanceLevels;
            AddFloors(floors);
            numberOfClearanceLevels = floors.Count + 1;
            Elevator elevator = new Elevator();
            Control control = new Control(elevator, floors[0]);

            int numberofSpawns = 5;
            #region Test SpawnedPersonnel
            // Create random personnel
            //SpawnPersonOnRandomFloor(1, floors, numberOfClearanceLevels);
            for (int i = 0; i < 5; i++)
            {
                personnel.Add(SpawnPersonOnRandomFloor(i, floors, numberOfClearanceLevels));
            }

            while (numberofSpawns > 0)
            {
                foreach (IPerson person in personnel)
                {
                    // Check if there's af person on floor and the person is first in line 
                    if (person.SpawnFloor.Personnel.Count > 0 && person == person.SpawnFloor.Personnel[0])
                    {
                        person.CallElevator(elevator);
                        person.SpawnFloor.scanner.ScanPerson(person);
                    }
                }
                
                elevator.ShowElevatorQueue();
                Floor floorThatCalled = control.MoveElevator();
                IPerson caller = floorThatCalled.Personnel[0];
                // Make sure that the elevator is in the correct floor
                if (caller.HasCalledElevator && elevator.CurrentFloor == caller.SpawnFloor)
                {
                    caller.EnterElevator(elevator);
                }

                control.RetrieveScanResult(floorThatCalled);

                // Check floors after elevator movement
                CheckFloorsForPersonnel(floors);
                numberofSpawns--;

                Console.ReadKey();
            }

            
            #endregion

            #region Test individual types of personnel

            // Create personnel with clearance
            //Personnel spawnedPerson = new Personnel(numberOfClearanceLevels, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(spawnedPerson);

            // Create personnel with insufficient clearance to target floor, and spawned floor is default floor
            //Personnel spawnedPerson = new Personnel(1, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(spawnedPerson);

            // Create personnel with insufficient clearance to target floor, and spawned floor is not default floor
            //Personnel spawnedPerson = new Personnel(2, floors[3], floors[1], 0);
            //floors[1].Personnel.Add(spawnedPerson);

            // Create personnel with insufficient clearance to spawned floor
            //Personnel spawnedPerson = new Personnel(1, floors[3], floors[2], 0);
            //floors[2].Personnel.Add(spawnedPerson);

            // Create intruder on floor 0
            //Personnel spawnedPerson = new Personnel(0, floors[3], floors[0], 0);
            //floors[0].Personnel.Add(spawnedPerson);

            /*
             * 
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
                   floors[i].scanner.ScanPerson(person);
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
           CheckFloorsForPersonnel(floors);
           */
            #endregion
        }

        public static void AddFloors(List<Floor> floors)
        {
            floors.Add(Factory.CreateFloor(1, "Ground"));
            floors.Add(Factory.CreateFloor(2, "B1"));
            floors.Add(Factory.CreateFloor(3, "B2"));
            floors.Add(Factory.CreateFloor(4, "B3"));
        }

        public static IPerson SpawnPersonOnRandomFloor(int id, List<Floor> floors, int numberOfClearanceLevels)
        {
            int randomFloor;
            if (floors.Count > 0)
            {
                randomFloor = rnd.Next(0, floors.Count);
                IPerson person = floors[randomFloor].SpawnNewPerson(numberOfClearanceLevels, floors[randomFloor], floors, id);
                return person;
            }
            return null;
        }

        public static void CheckFloorsForPersonnel(List<Floor> floors)
        {
            for (int i = 0; i < floors.Count; i++)
            {
                // A floor can only call elevator once (pushing call button multiple times won't stack in queue)
                // Only if there's a person on the floor the person can push the floor's call button
                if (floors[i].Personnel.Count != 0)
                {
                    Console.WriteLine($"[{floors[i].floorName}]: Next in line is {floors[i].Personnel[0].Id}");
                }
                else
                {
                    Console.WriteLine($"[{floors[i].floorName}]: No one on floor");
                }
            }
        }
    }
}
