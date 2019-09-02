using System;
using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Message;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class PlayerActor : ReceivePersistentActor
    {
        private readonly string _playerName;
        private int _health;

        public override string PersistenceId => $"player-{_playerName}";
        
        public PlayerActor(string playerName, int health)
        {
            _playerName = playerName;
            _health = health;
            
            WriteLine($"{_playerName} created");
            Command<HitMessage>(HitPlayer);
            Command<DisplayStatusMessage>(_ => DisplayPlayerStatus());
            Command<CauseErrorMessage>(_ => SimulateError());
            
            Recover<HitMessage>(message =>
            {
                WriteLine($"{_playerName} replaying HitMessage {message} from journal");
                _health -= message.Damage;
            });
        }

        private void HitPlayer(HitMessage message)
        {
            WriteLine($"{_playerName} received HitMessage");
            WriteLine($"{_playerName} persisting HitMessage");
            
            Persist(message, hitMessage =>
            {
                WriteLine($"{_playerName} persisted HitMessage ok, updating actor state");
                _health -= message.Damage;    
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