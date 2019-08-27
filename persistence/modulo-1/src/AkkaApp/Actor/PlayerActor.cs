using System;
using Akka.Actor;
using AkkaApp.Message;
using static AkkaApp.DisplayHelper;

namespace AkkaApp.Actor
{
    public class PlayerActor : ReceiveActor
    {
        private readonly string _playerName;
        private int _health;

        public PlayerActor(string playerName, int health)
        {
            this._playerName = playerName;
            this._health = health;
            
            WriteLine($"{this._playerName} created");
            Receive<HitMessage>(HitPlayer);
            Receive<DisplayStatusMessage>(_ => DisplayPlayerStatus());
            Receive<CauseErrorMessage>(_ => SimulateError());
        }

        private void HitPlayer(HitMessage message)
        {
            WriteLine($"{this._playerName} received HitMessage");
            this._health -= message.Damage;
        }

        private void DisplayPlayerStatus()
        {
            WriteLine($"{this._playerName} received DisplayStatusMessage");
            Console.WriteLine($"{this._playerName} has {this._health} health");
        }

        private void SimulateError()
        {
            WriteLine($"{this._playerName} received CauseErrorMessage");

            throw new ApplicationException($"Simulated exception in player: {this._playerName}");
        }
    }
}