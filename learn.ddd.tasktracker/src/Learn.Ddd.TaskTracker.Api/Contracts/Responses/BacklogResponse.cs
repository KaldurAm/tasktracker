using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Responses;

public record BacklogResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("productId")]
	Guid ProductId);

public record BacklogWithIssuesResponse([property : JsonProperty("id")]
	Guid Id,
	[property : JsonProperty("name")]
	string Name,
	[property : JsonProperty("productId")]
	Guid ProductId,
	[property : JsonProperty("issues")]
	List<IssueResponse> Issues);