using System;
using System.Threading;
using Akka.Actor;
using AkkaApp.Actors;
using AkkaApp.Messages;

using static AkkaApp.ColorConsole;
using static System.Console;

namespace AkkaApp
{
    class Program
    {
        private static ActorSystem _movieStreamActorSystem;

        private static void Main(string[] args)
        {
            WriteLineGray("Creating MovieStreamingActorSystem");
            _movieStreamActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            WriteLineGray("Creating actor supervisory hierarchy");
            _movieStreamActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");


            do
            {
                ShortPause();

                WriteLine();
                ForegroundColor = ConsoleColor.DarkGray;
                WriteLineGray("enter a command and hit enter");

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

                if (command == "exit")
                {
                    _movieStreamActorSystem.Terminate();
                    WriteLineGray("Actor system shutdown");
                    ReadKey();
                    Environment.Exit(1);
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