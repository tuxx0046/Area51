using System;
using System.Collections.Generic;
using System.Text;

namespace Area51
{
    public class Floor
    {
        private readonly IPanel _panel;
        private readonly IScanner _scanner;
        private readonly ITurret _turret;

        public Floor(IPanel panel, IScanner scanner, ITurret turret)
        {
            _panel = panel;
            _scanner = scanner;
            _turret = turret;
        }

        public void RelayKillOrder(IPerson person)
        {
            _turret.EliminateTarget(person);
        }

        
    }
}
