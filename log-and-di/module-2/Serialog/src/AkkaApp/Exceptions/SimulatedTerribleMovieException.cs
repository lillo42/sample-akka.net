using System;

namespace AkkaApp.Exceptions
{
    public class SimulatedTerribleMovieException : Exception
    {
        public string MovieTitle { get; }

        public SimulatedTerribleMovieException(string movieTitle) 
            : base($"{movieTitle} is a terrible movie")
        {
            MovieTitle = movieTitle;
        }
    }
}