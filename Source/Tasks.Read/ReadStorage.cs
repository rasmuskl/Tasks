using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;
using Tasks.Read.Models;
using Tasks.Read.Queries;
using System.Linq;

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

        public static Guid GetContextIdByName(Guid userId, string name)
        {
            var context = Query(new QueryContextsByUserId(userId)).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));

            if (context == null)
                return Guid.Empty;

            return context.ContextId;

        }

        public static IEnumerable<TaskReadModel> GetTasksByContextId(Guid userId, Guid contextId)
        {
            if (!Tasks.ContainsKey(userId))
                return new TaskReadModel[] { };

            return Tasks[userId].Where(x => x.ContextId == contextId);
        }

        public static IEnumerable<NoteReadModel> GetNotesByContextId(Guid userId, Guid contextId)
        {
            if (!Notes.ContainsKey(userId))
                return new NoteReadModel[] { };

            return Notes[userId].Where(x => x.ContextId == contextId);
        }

        public static IEnumerable<ContextReadModel> GetContextsExceptContextId(Guid userId, Guid contextId)
        {
            return Query(new QueryContextsByUserId(userId)).Where(x => x.ContextId != contextId);
        }

        public static ContextReadModel GetContextById(Guid userId, Guid contextId)
        {
            return Query(new QueryContextsByUserId(userId)).FirstOrDefault(x => x.ContextId == contextId);
        }
    }
}