using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using Machine.Specifications;
using Tasks.Events;
using System.Linq;
using Tasks.Write.CommandHandlers;
using Tasks.Write.Commands;

namespace Tasks.Tests.Write
{
    public class when_create_context_is_processed
    {
        static List<object> _events;
        static IStoreEvents _storeEvents;
        static CreateContext _createContext;

        Establish context = () =>
            {
                _events = new List<object>();

                _storeEvents = Wireup
                    .Init()
                    .UsingInMemoryPersistence()
                    .UsingSynchronousDispatcher(new DelegateMessagePublisher(x => _events.AddRange(x.Events.Select(e => e.Body).ToList())))
                    .Build();

                _createContext = new CreateContext(Guid.NewGuid(), "context", Guid.NewGuid());
            };

        Because of = () => new CreateContextHandler(_storeEvents).Handle(_createContext);

        It should_publish_one_event = () => _events.Count.ShouldEqual(1);

        It should_contain_a_create_context_event = () => _events.Count(x => x is ContextCreated).ShouldEqual(1);

        It should_have_the_correct_user_id =
            () => _events.OfType<ContextCreated>().First().UserId.ShouldEqual(_createContext.UserId);

        It should_have_the_correct_context_name =
            () => _events.OfType<ContextCreated>().First().ContextName.ShouldEqual(_createContext.ContextName);
        
        It should_have_the_correct_context_id =
            () => _events.OfType<ContextCreated>().First().ContextId.ShouldEqual(_createContext.ContextId);
    }
}