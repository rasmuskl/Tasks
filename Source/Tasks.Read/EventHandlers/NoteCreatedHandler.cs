using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class NoteCreatedHandler : IEventHandler<NoteCreated>
    {
        public void Handle(NoteCreated @event)
        {
            if(!ReadStorage.Notes.ContainsKey(@event.UserId))
            {
                ReadStorage.Notes[@event.UserId] = new List<Tuple<string, string>>();
            }

            ReadStorage.Notes[@event.UserId].Add(new Tuple<string, string>(@event.Title, @event.Description));
        }
    }
}