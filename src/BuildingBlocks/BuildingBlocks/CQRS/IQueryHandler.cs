using MediatR;

namespace BuildingBlocks.CQRS;

public interface IQueryHandler<in TQuery, TReospone>
    : IRequestHandler<TQuery, TReospone>
    where TQuery : ICommand<TReospone>
    where TReospone : notnull
{

}
