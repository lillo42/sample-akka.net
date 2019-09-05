using System;
using System.Threading;
using Akka.Actor;
using Akka.Configuration;
using AkkaApp.Actors;
using AkkaApp.Messages;
using static System.Console;

namespace AkkaApp
{
    class Program
    {
        private static ActorSystem _movieStreamActorSystem;
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"akka { 
                    loglevel = DEBUG
                    loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
            }");
            _movieStreamActorSystem = ActorSystem.Create("MovieStreamingActorSystem", config);
            _movieStreamActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");
            
            do
            {
                ShortPause();

                WriteLine();
                WriteLine("enter a command and hit enter");
                
                var command = ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    _movieStreamActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);                    

                    var message = new StopMovieMessage(userId);
                    _movieStreamActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

            } while (true);
        }

        // Perform a short pause for demo purposes to allow console to update nicely
        private static void ShortPause()
        {
            Thread.Sleep(450);
        }
    }
}
