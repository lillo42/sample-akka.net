using System;
using Akka.Actor;
using AkkaApp.Exceptions;

namespace AkkaApp.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
            Context.ActorOf(Props.Create<TrendingMoviesActor>(), "TrendingMovies");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is ActorInitializationException)
                    {
                        // TODO: log: PlaybackStatisticsActor PlaybackStatisticsActor supervisor strategy stopping child due to ActorInitializationException

                        return Directive.Stop;
                    }

                    if (exception is SimulatedTerribleMovieException terribleMovieEx)
                    {
                        // TODO: log: PlaybackStatisticsActor supervisor strategy resuming child due to terrible movie terribleMovieEx.MovieTitle

                        return Directive.Resume;
                    }
                    
                    // TODO: log: PlaybackStatisticsActor supervisor strategy restarting child due to unexpected exception
                    return Directive.Restart;
                }
                );
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            // TODO: log: PlaybackStatisticsActor PreStart
        }

        protected override void PostStop()
        {
            // TODO: log: PlaybackStatisticsActor PostStop
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // TODO: log: PlaybackStatisticsActor PreRestart because reason

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            // TODO: log: PlaybackStatisticsActor PostRestart because reason

            base.PostRestart(reason);
        }
        #endregion
    }

}