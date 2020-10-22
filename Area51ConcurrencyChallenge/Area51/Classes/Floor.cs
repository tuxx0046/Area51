﻿using Area51.Classes;
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
        private readonly string _floorName;
        public bool CallElevator { get; set; } = false;
        public Elevator Elevator { get; }

        public Floor(IPanel panel, IScanner scanner, ITurret turret, string floorName)
        {
            _panel = panel;
            _scanner = scanner;
            _turret = turret;
            _floorName = floorName;
            Elevator = elevator;
        }

        public void RelayKillOrder(IPerson person)
        {
            _turret.EliminateTarget(person, _floorName);
        }

        public IPerson SendScanResult()
        {
            return _scanner.SendScanResult();
        }
    }
}
