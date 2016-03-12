using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineInColor(
                $"PlayMovieMessage: '{message.MovieTitle}' for user {message.UserId}", 
                ConsoleColor.Yellow);
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineInColor("PlaybackActor PreStart", ConsoleColor.Green);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineInColor("PlaybackActor PostStop", ConsoleColor.Green);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineInColor("PlaybackActor PreRestart", ConsoleColor.Green);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineInColor("PlaybackActor PostRestart", ConsoleColor.Green);

            base.PostRestart(reason);
        }
    }
}
