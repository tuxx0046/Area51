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
            if (person.SecurityCertificate == 0)
            {
                person.MarkForTermination = true;
            }

            _scanResult = person;
        }

        public IPerson SendScanResult()
        {
            if (_scanResult != null)
            {
                return _scanResult;
            }

            return null;
        }
    }
}
