using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;

namespace Tasks.Tests.Read
{
    public class when_user_is_registered : ReadContext
    {
        static UserRegistered _userRegistered;

        Establish context = () =>
            {
                _userRegistered = new UserRegistered(Guid.NewGuid(), "test@test.dk", "1234");
            };

        Because of = () => ProcessedEvent(_userRegistered);

        It should_exist_when_querying_by_email = () => ReadStorage.Query(new QueryUserIdByEmail("test@test.dk")).ShouldEqual(_userRegistered.UserId);
        
        It should_exist_when_querying_by_email_with_other_casing = () => ReadStorage.Query(new QueryUserIdByEmail("TEST@TEST.DK")).ShouldEqual(_userRegistered.UserId);
    }
}