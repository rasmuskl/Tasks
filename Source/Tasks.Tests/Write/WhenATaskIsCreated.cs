using System;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Write.Commands;
using System.Linq;
using Machine.Specifications;

namespace Tasks.Tests.Write
{
    public class WhenATaskIsCreated : SQWriteContext
    {
        [Test]
        public void ShouldExistInContext()
        {
            Guid userId = Guid.NewGuid();

            Scenario(s => s.Given(ARegisteredUser_, userId)
                            .When(ATaskIsCreatedInGeneralContextForUser_, userId)
                            .Then(TaskCreatedShouldBeRaisedForUser_, userId));
        }

        public void ARegisteredUser_(Guid userId)
        {
            AddToHistory(userId, new UserRegistered(userId, "mail" + userId + "@gmail.com", string.Empty));
        }

        public void TaskCreatedShouldBeRaisedForUser_(Guid userId)
        {
            _eventsPublished.OfType<TaskCreated>().Count(x => x.UserId == userId).ShouldEqual(1);
        }

        public void ATaskIsCreatedInGeneralContextForUser_(Guid userId)
        {
            _executor.Execute(new CreateTask("test", userId));
        }
    }
}