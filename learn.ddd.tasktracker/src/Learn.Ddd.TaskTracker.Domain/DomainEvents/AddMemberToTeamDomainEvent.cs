using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.DomainEvents;

public record AddMemberToTeamDomainEvent(Guid TeamId, Guid MemberId) : IDomainEvent;