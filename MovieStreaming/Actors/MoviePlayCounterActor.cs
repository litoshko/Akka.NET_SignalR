using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Exceptions;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementMessage(message));
        }

        private void HandleIncrementMessage(IncrementPlayCountMessage message)
        {
            if (_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }

            // Simulated bugs
            if (_moviePlayCounts[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if (message.MovieTitle == "Partial Recoil")
            {
                throw new SimulatedTerribleMovieException();
            }

            ColorConsole.WriteLineInColor(
                $"MoviePlayCounterActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times",
                ConsoleColor.Magenta);
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineInColor("MoviePlayCounterActor PreStart", ConsoleColor.DarkCyan);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineInColor("MoviePlayCounterActor PostStop", ConsoleColor.DarkCyan);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineInColor("MoviePlayCounterActor PreRestart", ConsoleColor.DarkCyan);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineInColor("MoviePlayCounterActor PostRestart", ConsoleColor.DarkCyan);

            base.PostRestart(reason);
        }
        #endregion
    }
}