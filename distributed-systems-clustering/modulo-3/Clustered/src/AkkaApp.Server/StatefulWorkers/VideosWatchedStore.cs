using System;
using System.Collections.Generic;
using System.Linq;
using Akka;
using Akka.Actor;
using Akka.Persistence;
using AkkaApp.Server.Events;

namespace AkkaApp.Server.StatefulWorkers
{
    public class VideosWatchedStore : PersistentActor
    {
        private ICollection<VideoWatchedEvent> _store = new List<VideoWatchedEvent>();
        
        protected override bool ReceiveRecover(object message)
        {
            return message.Match()
                .With<VideoWatchedEvent>(view => _store.Add(view))
                .With<SnapshotOffer>(offer =>
                {
                    _store = (ICollection<VideoWatchedEvent>) offer.Snapshot;
                    Console.WriteLine($"Recovered state with {_store.Count} views");
                }).WasHandled;
        }

        protected override bool ReceiveCommand(object message)
        {
            return message.Match()
                .With<VideoWatchedEvent>(view => { 
                    Persist(view, v =>
                    {
                        _store.Add(v);
                        SaveSnapshot(_store);
                    }); 
                    Console.WriteLine($"Persisting {nameof(VideoWatchedEvent)}. video: {view.VideoId} user: {view.UserId}");
                })
                .With<PreviouslyWatchedVideosRequest>(req =>
                {
                    Console.WriteLine(nameof(PreviouslyWatchedVideosRequest) + $" for user {req.Job.UserId}");
                    
                    var result = _store
                        .Where(e => e.UserId == req.Job.UserId)
                        .Select(e => e.VideoId)
                        .Distinct();

                    Sender.Tell(new PreviouslyWatchedVideosResponse(req.Job, result));
                }).WasHandled;
        }

        public override string PersistenceId => "ViewsStore";
    }
}