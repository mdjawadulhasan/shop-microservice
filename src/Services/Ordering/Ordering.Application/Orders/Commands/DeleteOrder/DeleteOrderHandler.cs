namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.OrderId);


        // </ summary >
        //     Finds an entity with the given primary key values.If an entity with the given primary key values
        //     is being tracked by the context, then it is returned immediately without making a request to the
        //     database. Otherwise, a query is made to the database for an entity with the given primary key values
        //     and this entity, if found, is attached to the context and returned. If no entity is found, then
        // </summary>

        var order = await dbContext.Orders
            .FindAsync([orderId], cancellationToken: cancellationToken);   // Best with PK
        


        if (order is null)
        {
            throw new OrderNotFoundException(command.OrderId);
        }

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}
