namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record CreateIssueRequest(Guid BacklogId, string Title, string Description, int PriorityId, int TypeId, int State, int Estimation);