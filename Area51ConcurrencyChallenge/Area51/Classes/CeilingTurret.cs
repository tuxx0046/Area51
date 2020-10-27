using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Area51
{
    public class CeilingTurret : ITurret
    {
        /// <summary>
        /// Kill target person
        /// </summary>
        /// <param name="person"></param>
        /// <param name="floorName"></param>
        public void EliminateTarget(IPerson person, string floorName)
        {
            // Make sure that the target should be eliminated
            if (person.MarkedForTermination)
            {
                Console.WriteLine($"[Turret] {floorName} turret commencing target elimination...");
                // Takes time to kill target
                Thread.Sleep(3000);

                person.Die();
            }
            // SHOULD NOT HAPPEN. Check code if it does
            else
            {
                Console.WriteLine("Error. Target is not marked for elimination");
            }
        }

        public bool ConfirmKill(IPerson person)
        {
            if (person.IsDead)
            {
                Console.WriteLine($"[Turret]: Sends kill confirmation.");
                return true;
            }
            // This should not happen
            Console.WriteLine($"[Turret]: Target not killed.");
            return false;
        }
    }
}
