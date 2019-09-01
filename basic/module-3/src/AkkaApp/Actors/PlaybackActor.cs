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
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }


        protected override void PreStart() 
            => WriteLineGreen("PlayMovieMessage PreStart");

        protected override void PostStop() 
            => WriteLineGreen("PlaybackActor PostStop");

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