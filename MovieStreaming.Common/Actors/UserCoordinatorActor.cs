using System;
using System.Collections.Generic;
using Akka.Actor;
using MovieStreaming.Common.Messages;

namespace MovieStreaming.Common.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });

            Receive<StopMovieMessage>(
                message =>
                {
                    CreateChildUserIfNotExists(message.UserId);

                    IActorRef childActorRef = _users[message.UserId];

                    childActorRef.Tell(message);
                });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = 
                    Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);

                _users.Add(userId, newChildActorRef);

                ColorConsole.WriteLineInColor(
                    $"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {_users.Count})", 
                    ConsoleColor.Cyan);
            }
        }

        #region Lifecycle hooks
        protected override void PreStart()
        {
            ColorConsole.WriteLineInColor("UserCoordinatorActor PreStart", ConsoleColor.Cyan);
        }

        protected override void PostStop()
        {
            ColorConsole.WriteLineInColor("UserCoordinatorActor PostStop", ConsoleColor.Cyan);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineInColor("UserCoordinatorActor PreRestart", ConsoleColor.Cyan);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineInColor("UserCoordinatorActor PostRestart", ConsoleColor.Cyan);

            base.PostRestart(reason);
        }
        #endregion
    }
}