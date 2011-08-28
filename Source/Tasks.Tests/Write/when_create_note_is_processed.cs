using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using FakeItEasy;
using Machine.Specifications;
using Tasks.Events;
using System.Linq;
using Tasks.Write;
using Tasks.Write.CommandHandlers;
using Tasks.Write.Commands;

namespace Tasks.Tests.Write
{
    public class when_create_note_is_processed
    {
        static List<object> _eventsPublished = new List<object>();
        static IStoreEvents _storeEvents;

        Establish context = () =>
            {
                _storeEvents = Wireup
                    .Init()
                    .UsingInMemoryPersistence()
                    .UsingSynchronousDispatcher(new DelegateMessagePublisher(x => _eventsPublished.AddRange(x.Events.Select(e => e.Body).ToList())))
                    .Build();
            };

        Because of = () => new CreateNoteHandler(_storeEvents).Handle(new CreateNote("title", "description", Guid.NewGuid()));

        It should_publish_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_contain_a_create_note_event = () => _eventsPublished.Count(x => x is NoteCreated).ShouldEqual(1);
    }
}