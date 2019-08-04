using Akka.Actor;
using AkkaApp.Messages;
using static System.Console;

namespace AkkaApp.Actors
{
    public class PlaybackUntypedActor : UntypedActor
    {
        public PlaybackUntypedActor()
        {
            WriteLine("Creating a PlaybackUntypedActor");
        }

        protected override void OnReceive(object message)
        {
            if (message is PlayMovieMessage m)
            {
                WriteLine("Received movie title " + m.MovieTitle);
                WriteLine("Received user Id " + m.UserId);   
            }
            else
            {
                Unhandled(message);
            }
        }
    }
}