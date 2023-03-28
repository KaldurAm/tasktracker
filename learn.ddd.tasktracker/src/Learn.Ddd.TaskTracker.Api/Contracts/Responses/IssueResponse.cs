using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Api.Contracts.Responses;

public record IssueResponse(
	[property: JsonProperty("id")] string Id,
	[property: JsonProperty("title")] string Title,
	[property: JsonProperty("description")] string Description,
	[property: JsonProperty("estimation")] int Estimation,
	[property: JsonProperty("stateId")] int StateId,
	[property: JsonProperty("typeId")] int TypeId);