namespace AkkaApp.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(string movieTitle, int userId)
        {
            MovieTitle = movieTitle;
            UserId = userId;
        }

        public string MovieTitle { get; }
        public int UserId { get; }
    }
}