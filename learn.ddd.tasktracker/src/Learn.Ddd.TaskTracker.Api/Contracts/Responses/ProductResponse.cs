using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Responses;

public record ProductResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("description")]
	string Description);

public record ProductWithTeamResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("description")]
	string Description,
	[property : JsonProperty("team")]
	TeamResponse Team);

public record ProductWithBacklogResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("description")]
	string Description,
	[property : JsonProperty("backlog")]
	BacklogResponse Backlog);

public record ProductWithTeamAndBacklogResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("description")]
	string Description,
	[property : JsonProperty("team")]
	TeamResponse Team,
	[property : JsonProperty("backlog")]
	BacklogResponse Backlog);

public record ProductWithDetails([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("description")]
	string Description,
	[property : JsonProperty("team")]
	TeamWithMembersResponse Team,
	[property : JsonProperty("backlog")]
	BacklogWithIssuesResponse Backlog);