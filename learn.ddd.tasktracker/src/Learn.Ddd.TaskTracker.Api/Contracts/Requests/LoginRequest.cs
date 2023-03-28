using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record LoginRequest(
	[property: JsonProperty("login")] string Login,
	[property: JsonProperty("password")] string Password);