namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);

        var order = await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

        if (order is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrderWithNewValues(order, command.Order);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    public void UpdateOrderWithNewValues(Order order, OrderDto updatedOrderDto)
    {
        var updatedShippingAddress = Address.Of(updatedOrderDto.ShippingAddress.FirstName, updatedOrderDto.ShippingAddress.LastName, updatedOrderDto.ShippingAddress.EmailAddress, updatedOrderDto.ShippingAddress.AddressLine, updatedOrderDto.ShippingAddress.Country, updatedOrderDto.ShippingAddress.State, updatedOrderDto.ShippingAddress.ZipCode);
        var updatedBillingAddress = Address.Of(updatedOrderDto.BillingAddress.FirstName, updatedOrderDto.BillingAddress.LastName, updatedOrderDto.BillingAddress.EmailAddress, updatedOrderDto.BillingAddress.AddressLine, updatedOrderDto.BillingAddress.Country, updatedOrderDto.BillingAddress.State, updatedOrderDto.BillingAddress.ZipCode);
        var updatedPayment = Payment.Of(updatedOrderDto.Payment.CardName, updatedOrderDto.Payment.CardNumber, updatedOrderDto.Payment.Expiration, updatedOrderDto.Payment.Cvv, updatedOrderDto.Payment.PaymentMethod);

        order.Update(
            orderName: OrderName.Of(updatedOrderDto.OrderName),
            shippingAddress: updatedShippingAddress,
            billingAddress: updatedBillingAddress,
            payment: updatedPayment,
            status: updatedOrderDto.Status);
    }
}
