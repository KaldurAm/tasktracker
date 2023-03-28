using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public sealed record CreateTeamDomainEvent(Guid ProductId) : IDomainEvent;