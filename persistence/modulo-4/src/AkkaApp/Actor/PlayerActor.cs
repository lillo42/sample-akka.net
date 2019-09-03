using System;
using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Commands;
using AkkaApp.Events;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    internal class PlayerActorState
    {
        public string PlayerName { get; set; }
        public int Health { get; set; }

        public override string ToString()
        {
            return $"[PlayerActorState {PlayerName} {Health}]";
        }
    }

    public class  PlayerActor : ReceivePersistentActor
    {
        private PlayerActorState _state;
        private int _eventCounter;

        public override string PersistenceId => $"player-{_state}";
        
        public PlayerActor(string playerName, int health)
        {
            this._state = new PlayerActorState
            {
                PlayerName = playerName,
                Health = health
            };
            
            WriteLine($"{_state} created");
            Command<HitPlayer>(HitPlayer);
            Command<DisplayStatus>(_ => DisplayPlayerStatus());
            Command<SimulateError>(_ => SimulateError());
            
            Recover<HitPlayer>(message =>
            {
                WriteLine($"{_state} replaying HitMessage {message} from journal");
                _state.Health -= message.Damage;
            });
            
            Recover<SnapshotOffer>(offer =>
            {
                WriteLine($"{_state.PlayerName} received SnapshotOffer from snapshot store, updating state");
                
                _state = (PlayerActorState)offer.Snapshot;

                WriteLine($"{_state.PlayerName} state {_state} set from snapshot");
            });
        }

        private void HitPlayer(HitPlayer command)
        {
            WriteLine($"{_state} received HitMessage");

            var @event = new PlayerHit(command.Damage);
            
            WriteLine($"{_state} persisting HitMessage");
            
            Persist(@event, playerHitEvent =>
            {
                WriteLine($"{playerHitEvent} persisted HitMessage ok, updating actor state");
                _state.Health -= playerHitEvent.DamageTaken;

                _eventCounter++;
                if (_eventCounter == 5)
                {
                    WriteLine($"{_state.PlayerName} saving snapshot");
                    SaveSnapshot(_state);
                    WriteLine($"{_state.PlayerName} resetting event count to 0");
                    this._eventCounter = 0;
                }
            });
        }

        private void DisplayPlayerStatus()
        {
            WriteLine($"{_state} received DisplayStatusMessage");
            Console.WriteLine($"{_state} has {_state.Health} health");
        }

        private void SimulateError()
        {
            WriteLine($"{_state} received CauseErrorMessage");
            throw new ApplicationException($"Simulated exception in player: {_state}");
        }
    }
}