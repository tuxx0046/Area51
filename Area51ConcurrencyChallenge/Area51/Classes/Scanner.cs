using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace Area51
{
    public class Scanner : IScanner
    {
        private IPerson _scanResult;
        public void ScanPerson(IPerson person)
        {
            Console.WriteLine("[scanner]: " + person.SpawnFloor.FloorName + " scanner is scanning " + person.Id + "...");
            _scanResult = person;
        }

        /// <summary>
        /// Returns IPerson scan result
        /// </summary>
        /// <returns></returns>
        public IPerson SendScanResult()
        {
            if (_scanResult != null)
            {
                Console.WriteLine("[Scanner]: sending scan result from " + _scanResult.Id + " on " + _scanResult.SpawnFloor.FloorName);
                return _scanResult;
            }
            Console.WriteLine("[Scanner]: No scan result is found!");
            return null;
        }
    }
}
