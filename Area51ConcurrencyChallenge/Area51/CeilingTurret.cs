using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class CeilingTurret : ITurret
    {
        public void EliminateTarget(IPerson person)
        {
            // TODO: Must take time
            person.Die();
        }
    }
}
