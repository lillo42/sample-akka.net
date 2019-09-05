using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using AkkaApp.Messages;
using AkkaApp.Statistics;

namespace AkkaApp.Actors
{
    public class TrendingMoviesActor : ReceiveActor
    {
        private readonly ITrendingMovieAnalyzer _trendAnalyzer;

        private readonly Queue<string> _recentlyPlayedMovies;
        private const int NumberOfRecentMoviesToAnalyze = 5;

        public TrendingMoviesActor()
        {
            _recentlyPlayedMovies = new Queue<string>(NumberOfRecentMoviesToAnalyze);
            _trendAnalyzer = new SimpleTrendingMovieAnalyzer();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            var recentlyPlayedMoviesBufferIsFull = _recentlyPlayedMovies.Count == NumberOfRecentMoviesToAnalyze;

            if (recentlyPlayedMoviesBufferIsFull)
            {
                // remove oldest movie title
                _recentlyPlayedMovies.Dequeue();
            }

            _recentlyPlayedMovies.Enqueue(message.MovieTitle);

            LogDebug();
            
            var topMovie = _trendAnalyzer.CalculateMostPopularMovie(_recentlyPlayedMovies);

            // TODO: log: TrendingMovieActor Most popular movie trending now is topMovie
        }

        private void LogDebug()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Recently played movies:");

            foreach (var movie in _recentlyPlayedMovies)
            {
                sb.AppendLine(movie);
            }

            // TODO: log: TrendingMovieActor sb.ToString()            
        }

        #region Lifecycle hooks
        
        protected override void PreStart()
        {            
            // TODO: log: TrendingMovieActor PreStart
        }

        protected override void PostStop()
        {
            // TODO: log: TrendingMovieActor PostStop
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // TODO: log: TrendingMovieActor PreRestart because reason

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            // TODO: log: TrendingMovieActor PostRestart because reason

            base.PostRestart(reason);
        }
        #endregion
    }

}