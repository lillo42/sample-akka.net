using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaApp.Exceptions;
using AkkaApp.Messages;

namespace AkkaApp.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {            
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            //  Simulated bugs
            if (message.MovieTitle == "Partial Recoil")
            {
                throw new SimulatedTerribleMovieException(message.MovieTitle);
            }

            if (message.MovieTitle == "Partial Recoil 2")
            {
                throw new InvalidOperationException("Simulated exception");
            }

            // TODO: log: MoviePlayCounterActor message.MovieTitle has been watched _moviePlayCounts[message.MovieTitle] times
        }



        #region Lifecycle hooks

        protected override void PreStart()
        {
            // TODO: log: MoviePlayCounterActor PreStart
        }

        protected override void PostStop()
        {
            // TODO: log: MoviePlayCounterActor PostStop
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // TODO: log: MoviePlayCounterActor PreRestart because reason

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            // TODO: log: MoviePlayCounterActor PostRestart because reason

            base.PostRestart(reason);
        }
        #endregion
    }
}