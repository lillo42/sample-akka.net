using System.Collections.Generic;

namespace AkkaApp.Server.StatefulWorkers
{
    /// <summary>
    /// Response containing all videos a user has viewed
    /// </summary>
    internal class PreviouslyWatchedVideosResponse
    {
        public RecommendationJob Job { get; }

        public IEnumerable<int> PreviouslyViewedVideoIds { get; }

        public PreviouslyWatchedVideosResponse(RecommendationJob job, IEnumerable<int> previouslyViewedVideoIds)
        {
            Job = job;
            PreviouslyViewedVideoIds = previouslyViewedVideoIds;
        }
    }
}