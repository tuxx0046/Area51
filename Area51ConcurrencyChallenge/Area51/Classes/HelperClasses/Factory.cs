using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51
{
    public static class Factory
    {
        public static IPerson CreatePerson(int clearanceLevelCount, int numberOfFloors, int personnelId)
        {
            Random rnd = new Random();
            int clearanceLevel = rnd.Next(0, clearanceLevelCount);
            int spawnFloor = rnd.Next(0, numberOfFloors);
            int targetFloor;
            do
            {
                targetFloor = rnd.Next(0, numberOfFloors);
            }
            while (targetFloor == spawnFloor);

            return new Personnel(clearanceLevel, spawnFloor, targetFloor, personnelId);
        }

        public static Floor CreateFloor(string floorName)
        {
            return new Floor(CreatePanel(), CreateScannner(), CreateTurret(), floorName);
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
