using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Tasks.Events;
using Tasks.Write.Commands;
using Machine.Specifications;

namespace Tasks.Tests.Write
{
    public class NestTaskFixture : NUWriteContext
    {
        private Guid _task1Id;
        private Guid _task2Id;
        private Guid _userId;

        protected override void Given()
        {
            var userRegistered = new UserRegistered(Guid.NewGuid(), "prior-higher-test@test.dk", "1234");
            var task1Created = new TaskCreated("task 1", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now);
            var task2Created = new TaskCreated("task 2", Guid.NewGuid(), userRegistered.UserId, Guid.Empty, DateTime.Now);

            _userId = userRegistered.UserId;
            _task1Id = task1Created.TaskId;
            _task2Id = task2Created.TaskId;

            AddToHistory(_userId, userRegistered);
            AddToHistory(_task1Id, task1Created);
            AddToHistory(_task2Id, task2Created);
        }

        protected override void When()
        {
            _executor.Execute(new NestTask(_userId, _task2Id, _task1Id));
        }

        [Test]
        public void ShouldPublishAnEvent()
        {
            _eventsPublished.Count.ShouldEqual(1);
        }

        [Test]
        public void EventShouldContainCorrectParent()
        {
            var evt = _eventsPublished.First() as TaskNested;
            evt.ParentTaskId.ShouldEqual(_task1Id);
        }

        [Test]
        public void EventShouldContainCorrectTask()
        {
            var evt = _eventsPublished.First() as TaskNested;
            evt.TaskId.ShouldEqual(_task2Id);
        }

        [Test]
        public void EventShouldContainCorrectUser()
        {
            var evt = _eventsPublished.First() as TaskNested;
            evt.UserId.ShouldEqual(_task2Id);
        }
    }
}