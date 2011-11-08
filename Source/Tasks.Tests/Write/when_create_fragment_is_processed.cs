using System;
using System.Linq;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Write.Commands;

namespace Tasks.Tests.Write
{
    public class when_create_fragment_is_processed : WriteContext
    {
        static CreateFragment _create;

        Establish context = () =>
            {
                _create = new CreateFragment("description", Guid.NewGuid());
            };

        Because of = () => _executor.Execute(_create);

        It should_publish_one_event = () => _eventsPublished.Count.ShouldEqual(1);

        It should_contain_a_create_note_event = () => _eventsPublished.Count(x => x is FragmentCreated).ShouldEqual(1);
    }
}