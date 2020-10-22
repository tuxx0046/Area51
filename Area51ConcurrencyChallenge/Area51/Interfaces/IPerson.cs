namespace Area51
{
    public interface IPerson
    {
        string Id { get; }
        int SecurityCertificate { get; }
        int SpawnFloor { get; }
        int TargetFloor { get; }
        bool IsDead { get; set; }
        bool MarkedForTermination { get; set; }


        void Die();
        void GoToAdministration();
    }
}