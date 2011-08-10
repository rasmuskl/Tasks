﻿namespace Tasks.Model.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T @event);
    }
}