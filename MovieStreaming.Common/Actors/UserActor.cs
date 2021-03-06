﻿using System;
using Akka.Actor;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor(int userId)
        {
            Console.WriteLine("Creating a UserActor");

            ColorConsole.WriteLineInColor("Setting initial behaviour to stopped", ConsoleColor.Cyan);
            Stopped();
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ColorConsole.WriteLineInColor("Error: cannot start playing another movie before stopping existing one",
                    ConsoleColor.Red));
            Receive<StopMovieMessage>(
                message => StopPlayingCurrentMovie());

            ColorConsole.WriteLineInColor("UserActor has become playing", ConsoleColor.Cyan);
        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineInColor($"User nas stopped watching: '{_currentlyWatching}'", ConsoleColor.Yellow);

            _currentlyWatching = null;

            Become(Stopped);
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(
                message => ColorConsole.WriteLineInColor("Error: cannot stop if nothing is playing",
                    ConsoleColor.Red));

            ColorConsole.WriteLineInColor("UserActor has become stopped", ConsoleColor.Cyan);
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;

            ColorConsole.WriteLineInColor($"Currently watching: '{title}'", ConsoleColor.Yellow);

            Context.ActorSelection("/user/Playback/PlaybackStatistics/MoviePlayCounter")
                .Tell(new IncrementPlayCountMessage(title));

            Become(Playing);
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