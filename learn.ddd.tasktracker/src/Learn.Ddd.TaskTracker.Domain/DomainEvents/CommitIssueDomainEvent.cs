using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public record CommitIssueDomainEvent(Guid IssueId, Guid SprintId) : IDomainEvent;