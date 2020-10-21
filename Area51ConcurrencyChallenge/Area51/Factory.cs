using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51
{
    public class Factory
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

        public static IScanner CreateScannner()
        {
            return new Scanner();
        }
    }
}
