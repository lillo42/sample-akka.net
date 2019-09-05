using System.Collections.Generic;

namespace AkkaApp.Statistics
{
    public interface ITrendingMovieAnalyzer
    {
        string CalculateMostPopularMovie(IEnumerable<string> movieTitles);
    }
}