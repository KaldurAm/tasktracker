namespace Learn.Ddd.TaskTracker.Api.Contracts.Requests;

public record CreateProductRequest(string Name, string? Description = default);