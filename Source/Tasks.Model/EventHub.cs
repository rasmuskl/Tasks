using System;
using EventStore;
using StructureMap;
using Tasks.Model.Events;

namespace Tasks.Model
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

                Type openGenericType = typeof(IEventHandler<>);
                Type genericType = openGenericType.MakeGenericType(@event.GetType());

                var instances = _container.GetAllInstances(genericType);

                foreach (var instance in instances)
                {
                    var handleMethod = instance.GetType().GetMethod("Handle", new[] { @event.GetType() });
                    handleMethod.Invoke(instance, new[] {@event});
                }
            }
        }
    }
}