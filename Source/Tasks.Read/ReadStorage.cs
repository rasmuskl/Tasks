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
            Notes = new Dictionary<Guid, List<NoteReadModel>>();
            RegisteredEmails = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        internal static Dictionary<Guid, List<ContextReadModel>> Contexts { get; set; }
        internal static Dictionary<Guid, List<TaskReadModel>> Tasks { get; private set; }
        internal static Dictionary<Guid, List<NoteReadModel>> Notes { get; private set; }
        public static Dictionary<string, Guid> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }

        public static T Query<T>(IQuery<T> query)
        {
            var type = query.GetType();
            var returnType = typeof (T);

            var handlerType = typeof (IQueryHandler<,>);
            var genericHandlerType = handlerType.MakeGenericType(type, returnType);

            var handler = ObjectFactory.GetInstance(genericHandlerType);

            try
            {
                _lock.EnterReadLock();

                var handleMethod = handler.GetType().GetMethod("Handle", new[] { type });
                var result = handleMethod.Invoke(handler, new[] { query });

                return (T)result;
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