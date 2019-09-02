namespace AkkaApp.Commands
{
    internal class HitPlayer
    {
        public HitPlayer(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }
    }
}