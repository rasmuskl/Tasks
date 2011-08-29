using System;
using System.Collections.Generic;
using EventStore;
using StructureMap;
using Tasks.Read;

namespace Tasks.Write
{
    public static class Storage
    {
        private static IStoreEvents _store = ObjectFactory.GetInstance<IStoreEvents>();

        public static IStoreEvents Store
        {
            get { return _store; }
            set { _store = value; }
        }

        public static void Init()
        {
            IEnumerable<Commit> commits = _store.GetFrom(new DateTime(2000, 1, 1));

            foreach (var commit in commits)
            {
                ReadStorage.HandleCommit(commit);
            }
        }
    }
}