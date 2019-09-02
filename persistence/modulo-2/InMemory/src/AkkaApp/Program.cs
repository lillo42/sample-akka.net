using System;
using System.Threading;
using Akka.Actor;
using AkkaApp.Actor;
using AkkaApp.Message;
using static System.Console;

namespace AkkaApp
{
    internal static class Program
    {
        private static ActorSystem system;
        private static IActorRef playerCoordinator;

        private static void Main(string[] args)
        {
            system = ActorSystem.Create("Game");

            playerCoordinator = system.ActorOf<PlayerCoordinatorActor>("PlayerCoordinatorActor");
            
            ForegroundColor = ConsoleColor.White;
            
            DisplayInstructions();
            
            while (true)
            {
                ForegroundColor = ConsoleColor.White;

                var action = ReadLine();

                var playerName = action.Split(' ')[0];

                if (action.Contains("create"))
                {
                    CreatePlayer(playerName);
                }
                else if (action.Contains("hit"))
                {
                    var damage = int.Parse(action.Split(' ')[2]);

                    HitPlayer(playerName, damage);
                }
                else if(action.Contains("display"))
                {                    
                    DisplayPlayer(playerName);
                }                
                else if (action.Contains("error"))
                {
                    ErrorPlayer(playerName);
                }
                else
                {
                    WriteLine("Unknown command");
                }
            }
        }

        private static void ErrorPlayer(string playerName)
            => system.ActorSelection($"/user/PlayerCoordinatorActor/{playerName}")
                .Tell(new CauseErrorMessage());
        
        private static void DisplayPlayer(string playerName)
            => system.ActorSelection($"/user/PlayerCoordinatorActor/{playerName}")
                .Tell(new DisplayStatusMessage());
        
        private static void HitPlayer(string playerName, int damage)
            => system.ActorSelection($"/user/PlayerCoordinatorActor/{playerName}")
                .Tell(new HitMessage(damage));
        
        private static void CreatePlayer(string playerName)
            => playerCoordinator
                .Tell(new CreatePlayerMessage(playerName));
        
        private static void DisplayInstructions()
        {
            Thread.Sleep(2_000); // ensure console color set back to white
            ForegroundColor = ConsoleColor.White;

            WriteLine("Available commands:");
            WriteLine("<playername> create");
            WriteLine("<playername> hit");
            WriteLine("<playername> display");
            WriteLine("<playername> error");
        }
    }
}
