using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Area51.Tests
{
    [TestFixture]
    public class ScannerTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ScanPerson_StoreIPersonObjectAsScanResult_ScanResultEqualToIPersonInput()
        {
            var scannerTest = new Scanner();
            var person = new Personnel(1, 3, 1, 1);

            scannerTest.ScanPerson(person);

            Assert.That(scannerTest.SendScanResult(), Is.EqualTo(person));
        }
    }
}
