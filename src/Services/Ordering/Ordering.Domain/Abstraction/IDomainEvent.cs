﻿using MediatR;

namespace Ordering.Domain.Abstraction;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccuredOn => DateTime.UtcNow;
    public string EventTypeName => GetType().AssemblyQualifiedName;
}
