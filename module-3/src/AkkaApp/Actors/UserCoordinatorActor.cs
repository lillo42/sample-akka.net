using System;
using System.Collections.Generic;
using Akka.Actor;
using AkkaApp.Messages;
using static AkkaApp.ColorConsole;

namespace AkkaApp.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                var childActorRef = GetOrCreateChildUserIfNotExists(message.UserId);
                childActorRef.Tell(message);   
            });
            
            Receive<StopMovieMessage>(message =>
            {
                var childActorRef = GetOrCreateChildUserIfNotExists(message.UserId);
                childActorRef.Tell(message);   
            });
        }

        private IActorRef GetOrCreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef actorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), $"User{userId}");
                _users.Add(userId, actorRef);

                WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {_users.Count})");
            }
            
            return _users[userId];
        }
        
        #region Lifecycle hooks
        protected override void PreStart() 
            => WriteLineCyan("UserCoordinatorActor PreStart");

        protected override void PostStop() 
            => WriteLineCyan("UserCoordinatorActor PostStop");

        protected override void PreRestart(Exception reason, object message)
        {
            WriteLineCyan("UserCoordinatorActor PreRestart because: {0}", reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            WriteLineCyan("UserCoordinatorActor PostRestart because: {0}", reason);
            base.PostRestart(reason);
        } 
        #endregion
    }
}