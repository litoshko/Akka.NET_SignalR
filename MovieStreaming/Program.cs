using System;
using System.Threading;
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
            ColorConsole.WriteLineInColor("Actor system created", ConsoleColor.Gray);

            ColorConsole.WriteLineInColor("Creating actor supervisory hierarchy", ConsoleColor.Gray);
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();

                Console.WriteLine();
                ColorConsole.WriteLineInColor("enter a command and hit enter", ConsoleColor.Gray);

                var command = Console.ReadLine();

                if (command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command == "exit")
                {
                    MovieStreamingActorSystem.ActorSelection("*").Tell(PoisonPill.Instance);
                    MovieStreamingActorSystem.Terminate().Wait();
                    ColorConsole.WriteLineInColor("Actor system shutdown", ConsoleColor.Gray);
                    Console.ReadKey(true);
                    Environment.Exit(1);
                }
            } while (true);
        }

        private static void ShortPause()
        {
            Thread.Sleep(100);
        }
    }
}
