namespace AkkaApp.Server.Events
{
    public class VideoWatchedEvent
    {
        public int VideoId { get; }

        public int UserId { get; }

        public VideoWatchedEvent(int videoId, int userId)
        {
            VideoId = videoId;
            UserId = userId;
        }
    }
}