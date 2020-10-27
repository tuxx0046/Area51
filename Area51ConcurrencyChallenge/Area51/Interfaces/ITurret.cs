namespace Area51
{
    public interface ITurret
    {
        void EliminateTarget(IPerson person, string floorName);
        bool ConfirmKill(IPerson person);
    }
}