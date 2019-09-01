using Akka.Actor;
using AkkaApp.Messages;
using static System.Console;

namespace AkkaApp.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(message =>
            {
                WriteLine("Received movie title " + message.MovieTitle);
                WriteLine("Received user Id " + message.UserId);
            });
        }
    }
}