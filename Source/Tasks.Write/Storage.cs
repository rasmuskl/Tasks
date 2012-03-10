using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;
using Tasks.Read;

namespace Tasks.Write
{
    public static class Storage
    {
        public static IStoreEvents Store { get; set; }

        public static void Init(IContainer container)
        {
            Store = container.GetInstance<IStoreEvents>();

            IEnumerable<Commit> commits = Store.GetFrom(new DateTime(2000, 1, 1));

            foreach (var commit in commits)
            {
                ReadStorage.HandleCommit(commit);
            }
        }
    }
}