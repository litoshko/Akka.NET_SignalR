using System;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a UserActor");

            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            Receive<StopMovieMessage>(message => HandleStopMovieMessage());
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            if (_currentlyWatching != null)
            {
                ColorConsole.WriteLineInColor("Error: cannot start playing another movie before stopping existing one",
                    ConsoleColor.Red);
            }
            else
            {
                StartPlayingMovie(message.MovieTitle);
            }

        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            ColorConsole.WriteLineInColor($"Currently watching: '{title}'", ConsoleColor.Yellow);
        }

        private void HandleStopMovieMessage()
        {
            if (_currentlyWatching == null)
            {
                ColorConsole.WriteLineInColor("Error: cannot stop if nothing is playing",
                    ConsoleColor.Red);
            }
            else
            {
                ColorConsole.WriteLineInColor($"User has stopped watching: '{_currentlyWatching}'", ConsoleColor.Yellow);

                _currentlyWatching = null;
            }
        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineInColor("UserActor PreStart", ConsoleColor.Green);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineInColor("UserActor PostStop", ConsoleColor.Green);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineInColor("UserActor PreRestart", ConsoleColor.Green);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineInColor("UserActor PostRestart", ConsoleColor.Green);

            base.PostRestart(reason);
        }
    }
}