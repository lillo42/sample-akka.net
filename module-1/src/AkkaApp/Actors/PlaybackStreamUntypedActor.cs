using Akka.Actor;
using static System.Console;

namespace AkkaApp.Actors
{
    public class PlaybackStreamUntypedActor : UntypedActor
    {
        public PlaybackStreamUntypedActor()
        {
            WriteLine("Creating a PlaybackStreamUntypedActor");
        }
        
        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case string _:
                    WriteLine("Received movie title " + message);
                    break;
                case int _:
                    WriteLine("Received user ID " + message);
                    break;
                default:
                    Unhandled(message);
                    break;
            }
        }
    }
}