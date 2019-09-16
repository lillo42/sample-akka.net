namespace AkkaApp.Server.StatefulWorkers
{
    internal class PreviouslyWatchedVideosRequest
    {
        internal RecommendationJob Job { get; }

        internal PreviouslyWatchedVideosRequest(RecommendationJob job)
        {
            Job = job;
        }
    }
}