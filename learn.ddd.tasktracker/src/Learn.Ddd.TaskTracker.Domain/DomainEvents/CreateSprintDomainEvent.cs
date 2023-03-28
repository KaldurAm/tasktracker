using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public sealed record CreateSprintDomainEvent(Guid ProductId, string Title, string Goal, DateOnly Start, DateOnly Finish) : IDomainEvent;