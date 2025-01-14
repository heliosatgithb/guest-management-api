using System;

namespace GuestManagementApi.CQRS.Interfaces;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task Handle(TCommand command);
}
