using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommandHandler<in TCommand>
    : ICommandHandler<TCommand, Unit>
    where TCommand : ICommand<Unit>
{

}



public interface ICommandHandler<in TCommand, TReospone>
    : IRequestHandler<TCommand, TReospone>
    where TCommand : ICommand<TReospone>
    where TReospone : notnull
{

}
