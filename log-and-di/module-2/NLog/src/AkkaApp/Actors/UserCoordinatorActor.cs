using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaApp.Messages;

namespace AkkaApp.Actors
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

                // TODO: log: UserCoordinatorActor created new child UserActor for userId
                // TODO: log: Total Users _users.Count
            }
        }


        #region Lifecycle hooks
        protected override void PreStart()
        {
            // TODO: log: UserCoordinatorActor PreStart
        }

        protected override void PostStop()
        {
            // TODO: log: UserCoordinatorActor PostStop
        }

        protected override void PreRestart(Exception reason, object message)
        {
            // TODO: log: UserCoordinatorActor PreRestart because reason

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            // TODO: log: UserCoordinatorActor PostRestart because reason

            base.PostRestart(reason);
        } 
        #endregion
    }

}