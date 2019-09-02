using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Commands;
using AkkaApp.Events;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class PlayerCoordinatorActor : ReceivePersistentActor
    {
        private const int DefaultStartingHealth = 100;
        public override string PersistenceId => "player-coordinator";

        public PlayerCoordinatorActor()
        {
            Command<CreatePlayer>(message =>
            {
                WriteLine($"PlayerCoordinatorActor received CreatePlayer command for {message.PlayerName}");

                var @event = new PlayerCreated(message.PlayerName);
                
                Persist(@event, createPlayerEvent =>
                {
                    WriteLine($"PlayerCoordinatorActor persisted a CreatePlayerMessage for {createPlayerEvent.PlayerName}");
                    Context.ActorOf(
                        Props.Create(() => new PlayerActor(createPlayerEvent.PlayerName, DefaultStartingHealth)), createPlayerEvent.PlayerName); 
                });
            });
            
            Recover<PlayerCreated>(createPlayerEvent =>
            {
                WriteLine($"PlayerCoordinatorActor replaying CreatePlayerMessage for {createPlayerEvent.PlayerName}");

                Context.ActorOf(
                    Props.Create(() =>
                        new PlayerActor(createPlayerEvent.PlayerName, DefaultStartingHealth)), createPlayerEvent.PlayerName);
            });
        }
    }
}