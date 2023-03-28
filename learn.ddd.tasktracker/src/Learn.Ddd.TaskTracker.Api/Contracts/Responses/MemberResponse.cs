using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Responses;

public record MemberResponse(
	[property: JsonProperty("id")] string Id,
	[property: JsonProperty("firstName")] string FirstName,
	[property: JsonProperty("lastName")] string LastName,
	[property: JsonProperty("email")] string Email,
	[property: JsonProperty("alias")] string Alias);