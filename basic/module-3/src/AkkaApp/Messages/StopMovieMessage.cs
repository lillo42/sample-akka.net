namespace AkkaApp.Messages
{
    public class StopMovieMessage
    {
        public StopMovieMessage(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}