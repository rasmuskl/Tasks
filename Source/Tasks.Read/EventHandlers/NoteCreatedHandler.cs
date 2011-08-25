using System;
using System.Collections.Generic;
using Tasks.Events;

namespace Tasks.Read.EventHandlers
{
    public class NoteCreatedHandler : IEventHandler<NoteCreated>
    {
        public void Handle(NoteCreated evt)
        {
            if(!ReadStorage.Notes.ContainsKey(evt.UserId))
            {
                ReadStorage.Notes[evt.UserId] = new List<Tuple<string, string>>();
            }

            ReadStorage.Notes[evt.UserId].Add(new Tuple<string, string>(evt.Title, evt.Description));
        }
    }
}