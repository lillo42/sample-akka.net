using System;
using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Commands;
using AkkaApp.Events;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class  PlayerActor : ReceivePersistentActor
    {
        private readonly string _playerName;
        private int _health;

        public override string PersistenceId => $"player-{_playerName}";
        
        public PlayerActor(string playerName, int health)
        {
            _playerName = playerName;
            _health = health;
            
            WriteLine($"{_playerName} created");
            Command<HitPlayer>(HitPlayer);
            Command<DisplayStatus>(_ => DisplayPlayerStatus());
            Command<SimulateError>(_ => SimulateError());
            
            Recover<HitPlayer>(message =>
            {
                WriteLine($"{_playerName} replaying HitMessage {message} from journal");
                _health -= message.Damage;
            });
        }

        private void HitPlayer(HitPlayer command)
        {
            WriteLine($"{_playerName} received HitMessage");

            var @event = new PlayerHit(command.Damage);
            
            WriteLine($"{_playerName} persisting HitMessage");
            
            Persist(@event, playerHitEvent =>
            {
                WriteLine($"{playerHitEvent} persisted HitMessage ok, updating actor state");
                _health -= playerHitEvent.DamageTaken;
            });
        }

        private void DisplayPlayerStatus()
        {
            WriteLine($"{_playerName} received DisplayStatusMessage");
            Console.WriteLine($"{_playerName} has {this._health} health");
        }

        private void SimulateError()
        {
            WriteLine($"{this._playerName} received CauseErrorMessage");
            throw new ApplicationException($"Simulated exception in player: {_playerName}");
        }
    }
}