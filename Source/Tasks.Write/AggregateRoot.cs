using System.Collections.Generic;

namespace Tasks.Write
{
    public class AggregateRoot
    {
        public AggregateRoot()
        {
            UncommittedEvents = new List<object>();
        }

        public List<object> UncommittedEvents { get; private set; }
    }
}