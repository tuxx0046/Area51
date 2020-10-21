using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Personnel : IPerson
    {
        public string Id { get; }
        public int SecurityCertificate { get; }
        public int TargetFloor { get; }
        public int SpawnFloor { get; }
        public bool IsDead { get; set; } = false;
        public bool MarkForTermination { get; set; } = false;

        public Personnel(int clearanceLevel, int targetFloor, int spawnFloor, int staffId)
        {
            SecurityCertificate = clearanceLevel;
            TargetFloor = targetFloor;
            SpawnFloor = spawnFloor;
            Id = "Staff" + staffId.ToString();
            Console.WriteLine($"{Id} with clearance level {SecurityCertificate} " +
                $"just spawned at {SpawnFloor} floor and wants to go to {TargetFloor} floor");
        }

        public void Die()
        {

        }

        public void GoToAdministration()
        {

        }
    }
}
