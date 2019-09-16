using Akka.Actor;
using Akka.Configuration;

namespace AkkaApp
{
    internal static class Program
    {
        private static ActorSystem system;
        private static IActorRef playerCoordinator;

        private static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"akka {
                        actor { 
                            provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                        }
                        remote {
                            helios.tcp {
                                hostname = 127.0.0.1
                                port = 0
                            }
                        }
                        cluster {
                            seed-nodes = [""akka.tcp://Game@127.0.0.1:4053""]
                        }
                    }");
            system = ActorSystem.Create("Game");

            system.Terminate().GetAwaiter().GetResult();
        }
    }
}