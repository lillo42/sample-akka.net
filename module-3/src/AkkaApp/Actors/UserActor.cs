using System;
using Akka.Actor;
using AkkaApp.Messages;
using static AkkaApp.ColorConsole;

namespace AkkaApp.Actors
{
    public class UserActor : ReceiveActor
    {
        private readonly int _userId;
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            _userId = userId;

            Stopped();
        }
        
        private void Playing()
        {
            Receive<PlayMovieMessage>(
                message => WriteLineRed(
                    "UserActor {0} Error: cannot start playing another movie before stopping existing one", _userId));
           
            Receive<StopMovieMessage>(message => StopPlayingCurrentMovie());

            WriteLineYellow("UserActor {0} has now become Playing", _userId);
        }
        
        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
          
            Receive<StopMovieMessage>(
                message => WriteLineRed("UserActor {0} Error: cannot stop if nothing is playing", _userId));

            WriteLineYellow("UserActor {0} has now become Stopped", _userId);
        }
        
        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            WriteLineYellow("UserActor {0} is currently watching '{1}'", _userId, _currentlyWatching);

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
        }
        
        private void StopPlayingCurrentMovie()
        {
            WriteLineYellow("UserActor {0} has stopped watching '{1}'", _userId, _currentlyWatching);

            _currentlyWatching = null;

            Become(Stopped);
        }
        
        #region Lifecycle hooks
        protected override void PreStart() 
            => WriteLineYellow("UserActor {0} PreStart", _userId);

        protected override void PostStop() 
            => WriteLineYellow("UserActor {0} PostStop", _userId);

        protected override void PreRestart(Exception reason, object message)
        {
            WriteLineYellow("UserActor {0} PreRestart because: {1}", _userId, reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            WriteLineYellow("UserActor {0} PostRestart because: {1}", _userId, reason);
            base.PostRestart(reason);
        } 
        #endregion
    }
}