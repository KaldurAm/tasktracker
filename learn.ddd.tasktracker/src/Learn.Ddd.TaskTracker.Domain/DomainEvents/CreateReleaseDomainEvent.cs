using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public sealed record CreateReleaseDomainEvent(Guid ProductId, DateOnly ReleaseDate) : IDomainEvent;