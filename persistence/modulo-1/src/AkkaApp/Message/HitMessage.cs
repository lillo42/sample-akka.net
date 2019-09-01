namespace AkkaApp.Message
{
    internal class HitMessage
    {
        public HitMessage(int damage)
        {
            Damage = damage;
        }

        public int Damage { get; }
    }
}