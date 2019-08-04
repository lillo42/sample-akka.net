using Akka.Actor;
using AkkaApp.Actors;
using AkkaApp.Messages;
using static System.Console;

namespace AkkaApp
{
    class Program
    {
        private static ActorSystem _movieStreamActorSystem;
        static void Main(string[] args)
        {
            _movieStreamActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            
            WriteLine("Actor system created");
            
            TypedActor();

            UntypedActor();
            
            StreamUntypedActor();

            ReadLine();

            _movieStreamActorSystem.Terminate();
        }

        private static void TypedActor()
        {
            Props playbackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = _movieStreamActorSystem.ActorOf(playbackActorProps);

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The movie", 42));
        }

        private static void UntypedActor()
        {
            Props playbackUntypedActorProps = Props.Create<PlaybackUntypedActor>();

            IActorRef playbackUntypedActorRef =
                _movieStreamActorSystem.ActorOf(playbackUntypedActorProps, nameof(PlaybackUntypedActor));

            playbackUntypedActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie untyped", 43));
        }
        
        private static void StreamUntypedActor()
        {
            Props playbackStreamUntypedActorProps = Props.Create<PlaybackStreamUntypedActor>();

            IActorRef playbackStreamUntypedActorRef =
                _movieStreamActorSystem.ActorOf(playbackStreamUntypedActorProps, nameof(PlaybackStreamUntypedActor));

            playbackStreamUntypedActorRef.Tell("Akka.NET: The Movie Stream Untyped");
            playbackStreamUntypedActorRef.Tell(44);
            playbackStreamUntypedActorRef.Tell('c');
        }
    }
}
