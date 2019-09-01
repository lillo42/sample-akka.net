using System;
using Akka.Actor;

using static AkkaApp.ColorConsole;

namespace AkkaApp.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }
        
        #region Lifecycle hooks

        protected override void PreStart() 
            => WriteWhite("PlaybackStatisticsActor PreStart");

        protected override void PostStop() 
            => WriteWhite("PlaybackStatisticsActor PostStop");

        protected override void PreRestart(Exception reason, object message)
        {
            WriteWhite($"PlaybackStatisticsActor PreRestart because: {reason.Message}");
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            WriteWhite($"PlaybackStatisticsActor PostRestart because: {reason.Message}");
            base.PostRestart(reason);
        }
        
        #endregion
    }
}