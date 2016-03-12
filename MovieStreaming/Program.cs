using System;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        private static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created!");

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey(true);
            Console.WriteLine("Sending a PlayMovieMessage (Akka.NET: The Movie)");
            userActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            Console.ReadKey(true);
            Console.WriteLine("Sending a PlayMovieMessage (Partial Recall)");
            userActorRef.Tell(new PlayMovieMessage("Partial Recall", 99));
            Console.ReadKey(true);
            Console.WriteLine("Sending a StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());
            Console.ReadKey(true);
            Console.WriteLine("Sending another StopMovieMessage");
            userActorRef.Tell(new StopMovieMessage());

            // Send message to make actor terminate when processing it
            userActorRef.Tell(PoisonPill.Instance);

            // press any key to start shutdown of system
            Console.ReadKey(true);

            // tell actor system (and all its child actors) to shutdown
            MovieStreamingActorSystem.Terminate();

            // wait for actor to finish shutting down
            MovieStreamingActorSystem.WhenTerminated.Wait();
            Console.WriteLine("Actor System Terminated");

            // Press any key to stop console application
            Console.ReadKey(true);
        }
    }
}
