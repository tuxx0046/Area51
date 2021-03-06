﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Area51
{
    public static class Factory
    {
        static Random rnd = new Random();
        public static IPerson CreatePerson(int numberOfClearanceLevels, Floor spawnFloor, List<Floor> floors, int personnelId)
        {
            int clearanceLevel = rnd.Next(0, numberOfClearanceLevels);
            int targetFloor;
            // Don't allow target floor to be equal to spawn floor
            do
            {
                targetFloor = rnd.Next(0, floors.Count);
            }
            while (floors[targetFloor] == spawnFloor);

            return new Personnel(clearanceLevel, floors[targetFloor], spawnFloor, personnelId);
        }

        public static Floor CreateFloor(int floorLevel, string floorName)
        {
            return new Floor(CreateScannner(), CreateTurret(), floorLevel, floorName);
        }

        public static IScanner CreateScannner()
        {
            return new Scanner();
        }

        public static ITurret CreateTurret()
        {
            return new CeilingTurret();
        }
    }
}
