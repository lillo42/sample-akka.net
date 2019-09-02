namespace AkkaApp.Commands
{
    public class CreatePlayer
    {
        public CreatePlayer(string playerName)
        {
            PlayerName = playerName;
        }

        public string PlayerName { get; }
    }
}