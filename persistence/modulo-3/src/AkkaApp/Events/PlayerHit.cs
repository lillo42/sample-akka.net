namespace AkkaApp.Events
{
    public class PlayerHit
    {
        public PlayerHit(int damageTaken)
        {
            DamageTaken = damageTaken;
        }

        public int DamageTaken { get; }
    }
}