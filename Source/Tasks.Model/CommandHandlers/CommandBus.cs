using Tasks.Model.Commands;

namespace Tasks.Model.CommandHandlers
{
    internal class CommandBus
    {
        public void Handle(object command)
        {
            if(command is CreateTaskCommand)
            {
                new CreateTaskHandler().Handle(command as CreateTaskCommand);
            }

            if(command is RegisterUserCommand)
            {
                new RegisterUserHandler().Handle(command as RegisterUserCommand);
            }
        }
    }
}