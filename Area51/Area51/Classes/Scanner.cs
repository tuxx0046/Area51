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
            if (person != _scanResult)
            {
                Console.WriteLine("[Scanner]: " + person.SpawnFloor.floorName + " scanner is scanning " + person.Id + "...");
                _scanResult = person;
            }
        }

        /// <summary>
        /// Returns IPerson scan result
        /// </summary>
        /// <returns></returns>
        public IPerson SendScanResult()
        {
            if (_scanResult != null)
            {
                Console.WriteLine("[Scanner]: Sending scan result of " + _scanResult.Id + " to Control.");
                return _scanResult;
            }
            Console.WriteLine("[Scanner]: No scan result is found!");
            return null;
        }
    }
}
