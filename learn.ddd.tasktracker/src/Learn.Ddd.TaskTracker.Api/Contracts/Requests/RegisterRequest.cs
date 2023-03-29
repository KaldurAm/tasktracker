using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record RegisterRequest([property : JsonProperty("firstName")]
	string FirstName,
	[property : JsonProperty("lastName")]
	string LastName,
	[property : JsonProperty("login")]
	string Login,
	[property : JsonProperty("password")]
	string Password,
	[property : JsonProperty("confirmPassword")]
	string ConfirmPassword);