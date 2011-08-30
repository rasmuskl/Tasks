using System;
using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class NoteCreatedHandler : IEventHandler<NoteCreated>
    {
        public void Handle(NoteCreated evt)
        {
            if(!ReadStorage.Notes.ContainsKey(evt.UserId))
            {
                ReadStorage.Notes[evt.UserId] = new List<NoteReadModel>();
            }

            ReadStorage.Notes[evt.UserId].Add(new NoteReadModel { Title = evt.Title, Description = evt.Description });
        }
    }
}