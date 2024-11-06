using MediatR;

namespace BuildingBlocks.CQRS
{
    internal interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}
