using System;
using Akka.Actor;
using MovieStreaming.Common;

namespace MovieStreaming.Remote
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            ColorConsole.WriteLineInColor("Creating MovieStreamingActorSystem in remote process", ConsoleColor.Gray);

            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            MovieStreamingActorSystem.WhenTerminated.Wait();
        }
    }
}
