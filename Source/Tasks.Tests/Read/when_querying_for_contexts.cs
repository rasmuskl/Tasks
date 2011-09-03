using System;
using Machine.Specifications;
using Tasks.Events;
using Tasks.Read;
using Tasks.Read.Queries;
using System.Linq;

namespace Tasks.Tests.Read
{
    public class when_querying_for_contexts : ReadContext
    {
        static ContextCreated _contextCreated;

        Establish context = () =>
            {
                var userRegistered = WithEvent(new UserRegistered(Guid.NewGuid(), "test@test.dk", "1234"));

                _contextCreated = new ContextCreated(Guid.NewGuid(), "Context 1", userRegistered.UserId, DateTime.Now);
            };

        Because of = () => WithEvent(_contextCreated);

        It should_find_context_by_user_id = () => 
            ReadStorage.Query(new QueryContextsByUserId(_contextCreated.UserId))
            .Count(x => x.ContextId == _contextCreated.ContextId).ShouldEqual(1);

        It should_find_a_context_id_by_name_for_user = () =>
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "Context 1"))
                .ShouldEqual(_contextCreated.ContextId);
        
        It should_find_a_context_id_by_name_for_user_with_different_casing =() => 
            ReadStorage.Query(new QueryContextIdByName(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldEqual(_contextCreated.ContextId);

        It should_find_a_context_by_name_for_user = () => 
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "Context 1"))
                .ShouldBeTrue();        
        
        It should_find_a_context_by_name_for_user_with_different_casing =() => 
            ReadStorage.Query(new QueryUserHasContextNamed(_contextCreated.UserId, "CONTEXT 1"))
                .ShouldBeTrue();

        It should_find_context_by_id = () =>
            ReadStorage.Query(new QueryContextById(_contextCreated.UserId, _contextCreated.ContextId)).ShouldNotBeNull();

        It should_find_context_when_getting_contexts_except_general_context = () =>
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, Guid.Empty))
            .First().ContextId.ShouldEqual(_contextCreated.ContextId);

        It should_not_find_context_when_getting_contexts_except_new_context = () =>
            ReadStorage.Query(new QueryContextsExceptContext(_contextCreated.UserId, _contextCreated.ContextId))
            .ShouldNotContain(x => x.ContextId == _contextCreated.ContextId);
    }
}