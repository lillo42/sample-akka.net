using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Message;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class PlayerCoordinatorActor : ReceivePersistentActor
    {
        private const int DefaultStartingHealth = 100;
        public override string PersistenceId => "player-coordinator";

        public PlayerCoordinatorActor()
        {
            Command<CreatePlayerMessage>(message =>
            {
                WriteLine($"PlayerCoordinatorActor received CreatePlayerMessage for {message.PlayerName}");
                Persist(message, createPlayerMessage =>
                {
                    WriteLine($"PlayerCoordinatorActor persisted a CreatePlayerMessage for {message.PlayerName}");
                    Context.ActorOf(
                        Props.Create(() => new PlayerActor(message.PlayerName, DefaultStartingHealth)), message.PlayerName); 
                });
            });
            
            Recover<CreatePlayerMessage>(createPlayerMessage =>
            {
                WriteLine($"PlayerCoordinatorActor replaying CreatePlayerMessage for {createPlayerMessage.PlayerName}");

                Context.ActorOf(
                    Props.Create(() =>
                        new PlayerActor(createPlayerMessage.PlayerName, DefaultStartingHealth)), createPlayerMessage.PlayerName);
            });
        }
    }
}