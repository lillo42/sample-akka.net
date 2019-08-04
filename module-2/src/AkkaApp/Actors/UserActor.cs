using System;
using Akka.Actor;
using AkkaApp.Messages;

using static System.Console;
using static AkkaApp.ColorConsole;

namespace AkkaApp.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            WriteLine("Creating a UserActor");

            Receive<PlayMovieMessage>(HandlePlayMovieMessage);
            Receive<PlayMovieMessage>(message => StopPlayingCurrentMovie());
        }
        
        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                WriteLineRed(
                    "Error: cannot start playing another movie before stopping existing one");
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
        }


        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                WriteLineRed("Error: cannot stop if nothing is playing");
            }
            else
            {
                StopPlayingCurrentMovie();
            }
        }
        
        private void StopPlayingCurrentMovie()
        {
            WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");

            _currentlyWatching = null;
        }


        protected override void PreStart()
        {
            WriteLineGreen("UserActor PreStart");
        }

        protected override void PostStop()
        {
            WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            WriteLineGreen("UserActor PreRestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            WriteLineGreen("UserActor PostRestart because: " + reason);
            base.PostRestart(reason);
        }
    }
    
}