using System;
using Machine.Specifications;
using Tasks.Events;
using System.Linq;
using Tasks.Write.Commands;

namespace Tasks.Tests.Write
{
    public class when_create_context_is_processed : WriteContext
    {
        private static CreateContext _createContext;

        Establish context = () =>
            {
                _createContext = new CreateContext(Guid.NewGuid(), "context", Guid.NewGuid());
            };

        Because of = () => _executor.Execute(_createContext);

        It should_publish_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_contain_a_create_context_event = () => _eventsPublished.Count(x => x is ContextCreated).ShouldEqual(1);

        It should_have_the_correct_user_id =
            () => _eventsPublished.OfType<ContextCreated>().First().UserId.ShouldEqual(_createContext.UserId);

        It should_have_the_correct_context_name =
            () => _eventsPublished.OfType<ContextCreated>().First().ContextName.ShouldEqual(_createContext.ContextName);
        
        It should_have_the_correct_context_id =
            () => _eventsPublished.OfType<ContextCreated>().First().ContextId.ShouldEqual(_createContext.ContextId);
    }
}