using System.Collections.Generic;
using Tasks.Events;
using Tasks.Read.Models;

namespace Tasks.Read.EventHandlers
{
    public class FragmentCreatedHandler : IEventHandler<FragmentCreated>
    {
        public void Handle(FragmentCreated evt)
        {
            if(!ReadStorage.Fragments.ContainsKey(evt.UserId))
            {
                ReadStorage.Fragments[evt.UserId] = new List<FragmentReadModel>();
            }

            var models = ReadStorage.Fragments[evt.UserId];

            models.Add(new FragmentReadModel(evt.FragmentId, evt.Text, evt.UtcCreated));
        }
    }
}