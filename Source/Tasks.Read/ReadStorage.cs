using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;
using Tasks.Read.Models;

namespace Tasks.Read
{
    public static class ReadStorage
    {
        static ReadStorage()
        {
            Contexts = new Dictionary<Guid, List<ContextReadModel>>();
            Tasks = new Dictionary<Guid, List<TaskReadModel>>();
            Notes = new Dictionary<Guid, List<NoteReadModel>>();
            RegisteredEmails = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static void HandleCommit(Commit commit)
        {
            ObjectFactory.GetInstance<EventHub>().HandleCommit(commit);
        }

        internal static Dictionary<Guid, List<ContextReadModel>> Contexts { get; set; }
        public static Dictionary<Guid, List<TaskReadModel>> Tasks { get; private set; }
        public static Dictionary<Guid, List<NoteReadModel>> Notes { get; private set; }
        public static Dictionary<string, Guid> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }

        public static T Query<T>(IQuery<T> query)
        {
            var type = query.GetType();
            var returnType = typeof (T);

            var handlerType = typeof (IQueryHandler<,>);
            var genericHandlerType = handlerType.MakeGenericType(type, returnType);

            var handler = ObjectFactory.GetInstance(genericHandlerType);

            var handleMethod = handler.GetType().GetMethod("Handle", new [] { type });
            var result = handleMethod.Invoke(handler, new[] {query});

            return (T)result;
        }
    }
}