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
            Notes = new List<Tuple<string, string>>();
            RegisteredEmails = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            PasswordHashes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public static void HandleCommit(Commit commit)
        {
            ObjectFactory.GetInstance<EventHub>().HandleCommit(commit);
        }

        public static List<string> Tasks { get; private set; }
        public static List<Tuple<string, string>> Notes { get; private set; }
        public static HashSet<string> RegisteredEmails { get; private set; }
        public static Dictionary<string, string> PasswordHashes { get; private set; }
    }
}