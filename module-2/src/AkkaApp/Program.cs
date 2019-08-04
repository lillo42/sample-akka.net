using System;
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
            
            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = _movieStreamActorSystem.ActorOf(userActorProps, nameof(UserActor));

            ReadKey();
            WriteLine("Sending a PlayMovieMessage (Codenan the Destroyer)");
            userActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 42));

            ReadKey();
            WriteLine("Sending another PlayMovieMessage (Boolean Lies)");
            userActorRef.Tell(new PlayMovieMessage("Boolean Lies", 42));

            ReadKey();
            WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            ReadKey();
            WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());



            // press any key to start shutdown of system
            ReadKey();


            // Tell actor system (and all child actors) to shutdown
            _movieStreamActorSystem.Terminate();
            // Wait for actor system to finish shutting down
//            _movieStreamActorSystem.AwaitTermination();
            WriteLine("Actor system shutdown");


            // Press any key to stop console application
            ReadKey();

        }
    }
}
