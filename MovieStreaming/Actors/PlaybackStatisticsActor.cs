using System;
using Akka.Actor;
using MovieStreaming.Exceptions;

namespace MovieStreaming.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if (exception is SimulatedCorruptStateException)
                    {
                        return Directive.Restart;
                    }
                    if (exception is SimulatedTerribleMovieException)
                    {
                        return Directive.Resume;
                    }

                    return Directive.Restart;
                });
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineInColor("PlaybackStatisticsActor PreStart", ConsoleColor.White);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineInColor("PlaybackStatisticsActor PostStop", ConsoleColor.White);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineInColor("PlaybackStatisticsActor PreRestart", ConsoleColor.White);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineInColor("PlaybackStatisticsActor PostRestart", ConsoleColor.White);

            base.PostRestart(reason);
        }
        #endregion
    }
}