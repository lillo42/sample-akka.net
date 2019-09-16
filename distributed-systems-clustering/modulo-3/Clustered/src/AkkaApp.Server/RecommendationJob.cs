using Akka.Actor;

namespace AkkaApp.Server
{
    /// <summary>
    /// Initiates the recommendation workflow for a user
    /// </summary>
    internal class RecommendationJob
    {
        public int UserId { get; }

        public IActorRef Client { get; }

        public RecommendationJob(int userId, IActorRef client)
        {
            UserId = userId;
            Client = client;
        }
    }
}