using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class CeilingTurret : ITurret
    {
        public void EliminateTarget(IPerson person, string floorName)
        {
            if (person.MarkedForTermination)
            {
                Console.WriteLine($"Turret on {floorName} commencing target elimination...");
                // TODO: Must take time
                person.Die();
            }
            // SHOULD NOT HAPPEN
            else
            {
                Console.WriteLine("Error. Target is not marked for elimination");
            }
        }
    }
}
