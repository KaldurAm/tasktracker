namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record AddMemberToTeamRequest(Guid TeamId, Guid MemberId);