using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class CeilingTurret : ITurret
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="person"></param>
        /// <param name="floorName"></param>
        public void EliminateTarget(IPerson person, string floorName)
        {
            // Make sure that the target should be eliminated
            if (person.MarkedForTermination)
            {
                Console.WriteLine($"Turret on {floorName} commencing target elimination...");
                // TODO: Must take time
                person.Die();
                // TODO: Send confirmed kill to control
            }
            // SHOULD NOT HAPPEN. Check code if it does
            else
            {
                Console.WriteLine("Error. Target is not marked for elimination");
            }
        }
    }
}
