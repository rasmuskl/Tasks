using System;
using System.Collections.Generic;
using MarkdownSharp;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class NoteCreatedHandler : IEventHandler<NoteCreated>
    {
        public void Handle(NoteCreated evt)
        {
            if (!ReadStorage.Notes.ContainsKey(evt.UserId))
            {
                ReadStorage.Notes[evt.UserId] = new List<NoteReadModel>();
            }

            ReadStorage.Notes[evt.UserId].Add(new NoteReadModel
            {
                NoteId = evt.NoteId,
                Title = evt.Title,
                DescriptionRaw = evt.Description,
                DescriptionHtml = new Markdown().Transform(evt.Description),
            });
        }
    }
}