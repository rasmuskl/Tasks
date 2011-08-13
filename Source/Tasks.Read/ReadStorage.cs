using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;

namespace Tasks.Read
{
    public static class ReadStorage
    {
        static ReadStorage()
        {
            Tasks = new List<string>();
            RegisteredEmails = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static void HandleCommit(Commit commit)
        {
            ObjectFactory.GetInstance<EventHub>().HandleCommit(commit);
        }

        public static List<string> Tasks { get; private set; }
        public static HashSet<string> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }
    }
}