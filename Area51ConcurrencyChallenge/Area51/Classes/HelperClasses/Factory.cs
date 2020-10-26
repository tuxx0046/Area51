using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51
{
    public static class Factory
    {
        public static IPerson CreatePerson(int numberOfClearanceLevels, Dictionary<int, Floor> floors, int personnelId)
        {
            Random rnd = new Random();
            int clearanceLevel = rnd.Next(0, numberOfClearanceLevels);
            Floor spawnFloor = floors[rnd.Next(0, floors.Count)];
            Floor targetFloor;
            // Don't allow target floor to be equal to spawn floor
            do
            {
                targetFloor = floors[rnd.Next(0, floors.Count)];
            }
            while (targetFloor == spawnFloor);

            return new Personnel(clearanceLevel, targetFloor, spawnFloor, personnelId);
        }

        public static Floor CreateFloor(int floorLevel, string floorName)
        {
            return new Floor(CreatePanel(), CreateScannner(), CreateTurret(), floorLevel, floorName);
        }

        public static IScanner CreateScannner()
        {
            return new Scanner();
        }

        public static ITurret CreateTurret()
        {
            return new CeilingTurret();
        }

        public static IPanel CreatePanel()
        {
            return new Panel();
        }
    }
}
