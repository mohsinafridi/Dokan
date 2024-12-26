namespace Order.Domain.Events;

public record OrderCreatedEvent(Models.Order order) : IDomainEvent;
