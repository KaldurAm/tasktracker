using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public record CreateIssueDomainEvent(Issue Issue) : IDomainEvent;