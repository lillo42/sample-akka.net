using System;
using Akka.Actor;
using Akka.Event;
using AkkaApp.Exceptions;

namespace AkkaApp.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();
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
                        _logger.Error(exception, "PlaybackStatisticsActor supervisor strategy stopping child due to ActorInitializationException");
                        return Directive.Stop;
                    }

                    if (exception is SimulatedTerribleMovieException terribleMovieEx)
                    {
                        _logger.Warning(
                            "PlaybackStatisticsActor supervisor strategy resuming child due to terrible movie {0}", 
                            terribleMovieEx.MovieTitle);

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
            _logger.Debug("PlaybackStatisticsActor PreStart");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("PlaybackStatisticsActor PostStop");

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("PlaybackStatisticsActor PreRestart because {0}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }

}