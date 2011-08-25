using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tasks.Write
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot()
        {
            UncommittedEvents = new List<object>();
        }

        protected void ApplyUncommitted(object evt)
        {
            ApplyEvent(evt);
            UncommittedEvents.Add(evt);
        }

        public void ApplyCommitted(object evt)
        {
            ApplyEvent(evt);
        }

        private void ApplyEvent(object evt)
        {
            MethodInfo applyMethodInfo = GetType().GetMethod("Apply", BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { evt.GetType() }, null);

            if (applyMethodInfo == null)
            {
                throw new InvalidOperationException("Aggregate of type " + GetType().Name + " couldn't apply event of type: " + evt.GetType().Name);
            }

            applyMethodInfo.Invoke(this, new[] { evt });
        }

        public List<object> UncommittedEvents { get; private set; }
    }
}