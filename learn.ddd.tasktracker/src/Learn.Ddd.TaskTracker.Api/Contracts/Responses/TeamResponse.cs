using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Responses;

public record TeamResponse(
	[property: JsonProperty("id")] Guid Id,
	[property: JsonProperty("name")] string Name,
	[property: JsonProperty("productId")] Guid ProductId);

public record TeamWithMembersResponse(
	[property: JsonProperty("id")] Guid Id,
	[property: JsonProperty("name")] string Name,
	[property: JsonProperty("productId")] Guid ProductId,
	[property: JsonProperty("members")] List<MemberResponse> Members);
	
	