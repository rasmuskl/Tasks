using System;
using System.Collections.Generic;
using Tasks.Write.Commands;

namespace Tasks.Write.CommandHandlers
{
    public class DelegateCommandHandler
    {
        private readonly IRepository _repository;
        private Dictionary<Type, Delegate> _registrations = new Dictionary<Type, Delegate>();

        public DelegateCommandHandler(IRepository repository)
        {
            _repository = repository;
            Register<RegisterUser, User>((c, e) => e.RegisterUser(c.UserId, c.Email, c.PasswordSha1));
        }

        void Register<TCommand, TEntity>(Action<TCommand, TEntity> handleAction)
        {
            _registrations.Add(typeof(TCommand), handleAction);
        }

        public bool Handle(Guid id, object command)
        {
            Delegate handler;
        
            if (!_registrations.TryGetValue(command.GetType(), out handler))
                return false;

            var entityType = handler.GetType().GetGenericArguments()[1];
            var aggregate = _repository.Get(entityType, id);

            handler.DynamicInvoke(command, aggregate);

            _repository.Commit(id, aggregate);
            return true;

        }
    }
}