using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;
using Tasks.Read.Models;
using Tasks.Read.Queries;

namespace Tasks.Read
{
    public static class ReadStorage
    {
        static ReadStorage()
        {
            Contexts = new Dictionary<Guid, List<ContextReadModel>>();
            Tasks = new Dictionary<Guid, List<TaskReadModel>>();
            Notes = new Dictionary<Guid, List<Tuple<string, string>>>();
            RegisteredEmails = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static void HandleCommit(Commit commit)
        {
            ObjectFactory.GetInstance<EventHub>().HandleCommit(commit);
        }

        internal static Dictionary<Guid, List<ContextReadModel>> Contexts { get; set; }
        public static Dictionary<Guid, List<TaskReadModel>> Tasks { get; private set; }
        public static Dictionary<Guid, List<Tuple<string, string>>> Notes { get; private set; }
        public static Dictionary<string, Guid> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }

        public static Guid GetUserIdByEmail(string email)
        {
            return RegisteredEmails[email];
        }

        public static List<ContextReadModel> GetContextsByUserId(Guid userId)
        {
            return new QueryContextsByUserId(userId).Query();
        }
    }
}