using System;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Write.Commands;
using System.Linq;
using Machine.Specifications;

namespace Tasks.Tests.Write
{
    public class WhenATaskIsCreated : NUWriteContext
    {
        private Guid _userId;

        protected override void Given()
        {
            _userId = Guid.NewGuid();
            AddToHistory(_userId, new UserRegistered(_userId, "mail" + _userId + "@gmail.com", string.Empty));
        }

        protected override void When()
        {
            _executor.Execute(new CreateTask("test", _userId));
        }
        
        [Test]
        public void ShouldExistInContext()
        {
            _eventsPublished.OfType<TaskCreated>().Count(x => x.UserId == _userId).ShouldEqual(1);
        }
    }
}