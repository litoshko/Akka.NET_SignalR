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

            Props playbackActorProps = Props.Create<PlaybackActor>();

            IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 42));
            playbackActorRef.Tell(new PlayMovieMessage("Partial Recall", 99));
            playbackActorRef.Tell(new PlayMovieMessage("Boolean lies", 77));
            playbackActorRef.Tell(new PlayMovieMessage("Codenan the Destroyer", 1));

            playbackActorRef.Tell(PoisonPill.Instance);

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
