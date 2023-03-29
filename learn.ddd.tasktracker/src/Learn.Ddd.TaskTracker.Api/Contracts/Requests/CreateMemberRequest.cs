using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record CreateMemberRequest([property : JsonProperty("firstName")]
	string FirstName,
	[property : JsonProperty("lastName")]
	string LastName,
	[property : JsonProperty("email")]
	string Email);