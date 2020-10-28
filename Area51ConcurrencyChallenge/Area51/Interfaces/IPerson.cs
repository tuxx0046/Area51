using Area51.Classes;

namespace Area51
{
    public interface IPerson
    {
        string Id { get; }
        int SecurityCertificate { get; }
        Floor SpawnFloor { get; }
        Floor TargetFloor { get; }
        bool IsDead { get; set; }
        bool MarkedForTermination { get; set; }
        bool HasCalledElevator { get; set; }

        void EnterElevator(Elevator elevator);
        void CallElevator(Elevator elevator);
        void Die();
    }
}