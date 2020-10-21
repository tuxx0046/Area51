namespace Area51
{
    public interface IPerson
    {
        string Id { get; }
        int SecurityCertificate { get; }
        int SpawnFloor { get; }
        int TargetFloor { get; }
        bool IsDead { get; set; }
        bool MarkForTermination { get; set; }


        void Die();
        void GoToAdministration();
    }
}