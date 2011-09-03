using System;
using EventStore;
using StructureMap;

namespace Tasks.Read
{
    public class EventHub
    {
        private readonly IContainer _container;

        public EventHub(IContainer container)
        {
            _container = container;
        }

        public void HandleCommit(Commit commit)
        {
            foreach (var eventMessage in commit.Events)
            {
                object @event = eventMessage.Body;

                var openHandlerType = typeof(IEventHandler<>);
                var closedHandlerType = openHandlerType.MakeGenericType(@event.GetType());

                var instances = _container.GetAllInstances(closedHandlerType);

                foreach (dynamic instance in instances)
                {
                    instance.Handle((dynamic)@event);
                }
            }
        }
    }
}