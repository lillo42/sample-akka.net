using System;
using Akka.Actor;
using Akka.Event;
using AkkaApp.Messages;

namespace AkkaApp.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;
        private ILoggingAdapter _logger = Context.GetLogger();
        
        public UserActor(int userId)
        {
            _userId = userId;

            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message =>
                {
                    _logger.Warning(
                        "UserActor {0} cannot start playing another movie before stopping existing one",
                        _userId);
                });
           
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            _logger.Info("UserActor {0} behaviour has now become Playing", _userId);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));

            Receive<StopMovieMessage>(
                message =>
                {
                    _logger.Warning(
                        "UserActor {0} cannot stop if nothing is playing",
                        _userId);
                });

            _logger.Info("UserActor {0} behaviour has now become Stopped", _userId);
        }
        
        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            _logger.Info("UserActor {0} is currently watching {1}",
                _userId,
                _currentlyWatching);

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));

            Context.ActorSelection("/user/Playback/PlaybackStatistics/TrendingMovies")
                .Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
        }

        private void StopPlayingCurrentMovie()
        {
            _logger.Info("UserActor {0} has stopped watching {1}",
                _userId,
                _currentlyWatching);

            _currentlyWatching = null;

            Become(Stopped);
        }



        #region Lifecycle hooks
        protected override void PreStart()
        {
            _logger.Debug("UserActor {0} PreStart", _userId);
        }

        protected override void PostStop()
        {
            _logger.Debug("UserActor {0} PostStop", _userId);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("UserActor {0} PreRestart because reason {1}", _userId, reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("UserActor {0} PostRestart because {1}", _userId, reason);

            base.PostRestart(reason);
        } 
        #endregion
    }

}