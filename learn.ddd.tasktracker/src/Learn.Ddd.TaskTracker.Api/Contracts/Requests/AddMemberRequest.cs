namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record AddMemberRequest(Guid TeamId, Guid MemberId);