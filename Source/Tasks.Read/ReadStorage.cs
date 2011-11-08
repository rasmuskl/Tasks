using System;
using System.Collections.Generic;
using System.Threading;
using EventStore;
using StructureMap;
using Tasks.Read.Models;

namespace Tasks.Read
{
    public static class ReadStorage
    {
        static ReaderWriterLockSlim _lock;

        static ReadStorage()
        {
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            Contexts = new Dictionary<Guid, List<ContextReadModel>>();
            Tasks = new Dictionary<Guid, List<TaskReadModel>>();
            Fragments = new Dictionary<Guid, List<FragmentReadModel>>();
            CompletedTasks = new Dictionary<Guid, List<TaskReadModel>>();
            Notes = new Dictionary<Guid, List<NoteReadModel>>();
            RegisteredEmails = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        internal static Dictionary<Guid, List<ContextReadModel>> Contexts { get; set; }
        internal static Dictionary<Guid, List<TaskReadModel>> Tasks { get; private set; }
        internal static Dictionary<Guid, List<NoteReadModel>> Notes { get; private set; }
        internal static Dictionary<Guid, List<FragmentReadModel>> Fragments { get; private set; }
        public static Dictionary<string, Guid> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }
        internal static Dictionary<Guid, List<TaskReadModel>> CompletedTasks { get; private set; }

        public static TReturn Query<TReturn>(IQuery<TReturn> query)
        {
            var type = query.GetType();
            var returnType = typeof (TReturn);

            var openHandlerType = typeof (IQueryHandler<,>);
            var closedHandlerType = openHandlerType.MakeGenericType(type, returnType);

            dynamic handler = ObjectFactory.GetInstance(closedHandlerType);

            try
            {
                _lock.EnterReadLock();

                return (TReturn)handler.Handle((dynamic) query);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public static void HandleCommit(Commit commit)
        {
            try
            {
                _lock.EnterWriteLock();
                ObjectFactory.GetInstance<EventHub>().HandleCommit(commit);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}