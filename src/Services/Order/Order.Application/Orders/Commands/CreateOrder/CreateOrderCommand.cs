
using FluentValidation;
using Order.Application.Dtos;

namespace Order.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid OrderId);


public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer id is required");
        //RuleFor(x => x.Order.ShippingAddress).NotNull();
        //RuleFor(x => x.Order.BillingAddress).NotNull();
       // RuleFor(x => x.Order.Payment).NotNull();
        RuleFor(x => x.Order.OrderItems).NotNull().WithMessage("Order items are required");
        //RuleFor(x => x.Order.OrderItems).Must(x => x.Any()).WithMessage("Order must have at least one item");
    }
}