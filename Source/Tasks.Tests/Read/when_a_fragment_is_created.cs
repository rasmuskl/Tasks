using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_a_fragment_is_created : ReadContext
    {
        static FragmentCreated _created;

        Establish context = () =>
            {
                var userRegistered = ProcessedEvent(new UserRegistered(Guid.NewGuid(), Guid.NewGuid() + "@test.dk", "1234"));

                _created = new FragmentCreated(Guid.NewGuid(), "some text", userRegistered.UserId, DateTime.UtcNow);
            };

        Because of = () => ProcessedEvent(_created);

        It should_find_fragment_in_user = () =>
                ReadStorage.Query(new QueryFragmentsByUserId(_created.UserId))
                    .ShouldContain(x => x.FragmentId == _created.FragmentId);

        It should_not_find_fragment_in_another_user = () =>
                ReadStorage.Query(new QueryFragmentsByUserId(Guid.NewGuid()))
                    .ShouldNotContain(x => x.FragmentId == _created.FragmentId);
    }
}