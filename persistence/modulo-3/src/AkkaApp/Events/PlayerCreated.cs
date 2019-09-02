namespace AkkaApp.Events
{
    public class PlayerCreated
    {
        public PlayerCreated(string playerName)
        {
            PlayerName = playerName;
        }

        public string PlayerName { get; }
    }
}