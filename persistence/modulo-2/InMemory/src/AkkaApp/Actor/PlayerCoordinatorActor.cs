using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Message;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class PlayerCoordinatorActor : ReceiveActor
    {
        private const int DefaultStartingHealth = 100;

        public PlayerCoordinatorActor()
        {
            Receive<CreatePlayerMessage>(message =>
            {
                WriteLine($"PlayerCoordinatorActor received CreatePlayerMessage for {message.PlayerName}");
                Context.ActorOf(
                    Props.Create(() => new PlayerActor(message.PlayerName, DefaultStartingHealth)), message.PlayerName);
            });
        }
    }
}