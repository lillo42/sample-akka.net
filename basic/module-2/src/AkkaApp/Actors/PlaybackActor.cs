using System;
using Akka.Actor;
using AkkaApp.Messages;
using static System.Console;
using static AkkaApp.ColorConsole;

namespace AkkaApp.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            WriteLine("Creating PlaybackActor");

            Receive<PlayMovieMessage>(message =>
            {
                WriteLineYellow($"PlayMovieMessage '{message.MovieTitle}' for user {message.UserId}");
            });
        }


        protected override void PreStart()
        {
            WriteLineGreen("PlayMovieMessage PreStart");
        }

        protected override void PostStop()
        {
            WriteLineGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            WriteLineGreen("PlaybackActor PreRestart because: " + reason);

            base.PreRestart(reason, message);
        }
        
        protected override void PostRestart(Exception reason)
        {
            WriteLineGreen("PlaybackActor PostRestart because: " + reason);

            base.PostRestart(reason);
        }
    }
}