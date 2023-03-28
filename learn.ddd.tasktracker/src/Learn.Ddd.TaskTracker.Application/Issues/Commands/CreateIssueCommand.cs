using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Issues.Commands;

public class CreateIssueCommand : IRequest<Result<Issue>>
{
	public CreateIssueCommand(Guid backlogId, string title, string description, int priorityId, int typeId, int state, int estimation)
	{
		BacklogId = backlogId;
		Title = title;
		Description = description;
		PriorityId = priorityId;
		TypeId = typeId;
		State = state;
		Estimation = estimation;
	}

	public Guid BacklogId { get; }
	public string Title { get; }
	public string Description { get; }
	public int PriorityId { get; }
	public int TypeId { get; }
	public int State { get; }
	public int Estimation { get; }
}