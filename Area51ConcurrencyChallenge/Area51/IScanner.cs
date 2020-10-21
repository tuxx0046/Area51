namespace Area51
{
    public interface IScanner
    {
        void ScanPerson(IPerson person);
        IPerson SendScanResult();
    }
}