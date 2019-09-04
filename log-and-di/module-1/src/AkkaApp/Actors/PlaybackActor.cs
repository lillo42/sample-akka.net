using System;
using Akka.Actor;

namespace AkkaApp.Actors
{
    public class PlaybackActor : ReceiveActor
    {     
        public PlaybackActor()
        {           
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");        
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            // TODO: log: PlaybackActor PreStart
        }

        protected override void PostStop()
        {
            // TODO: log: PlaybackActor PostStop
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // TODO: log: PlaybackActor PreRestart because reason

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            // TODO: log: PlaybackActor PostRestart because reason

            base.PostRestart(reason);
        } 
        #endregion
    }

}