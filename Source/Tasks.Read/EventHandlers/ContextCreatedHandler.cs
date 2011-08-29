using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class ContextCreatedHandler : IEventHandler<ContextCreated>
    {
        public void Handle(ContextCreated evt)
        {
            if(!ReadStorage.Contexts.ContainsKey(evt.UserId))
                ReadStorage.Contexts[evt.UserId] = new List<ContextReadModel>();

            ReadStorage.Contexts[evt.UserId].Add(new ContextReadModel { ContextId = evt.ContextId, Name = evt.ContextName });
        }
    }
}