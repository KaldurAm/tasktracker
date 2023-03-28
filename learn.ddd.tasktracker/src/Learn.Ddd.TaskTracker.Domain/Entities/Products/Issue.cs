using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class Issue : AggregateRoot
{
	private readonly HashSet<Issue> _childIssues = new();

	/// <inheritdoc />
	public Issue(Guid id, Guid backlogId, string title, string description, int priorityId, int typeId, int stateId, int estimation) : base(id)
	{
		BacklogId = backlogId;
		Title = title;
		Description = description;
		PriorityId = priorityId;
		TypeId = typeId;
		StateId = stateId;
		Estimation = estimation;
	}

	public Guid BacklogId { get; init; }
	public string Title { get; init; }
	public string? Description { get; init; }
	public int PriorityId { get; init; }
	public int TypeId { get; init; }
	public int StateId { get; init; }
	public int Estimation { get; init; }
	public Guid? LinkedIssueId { get; set; }

	public virtual Backlog? Backlog { get; init; }

	public virtual Issue? LinkedIssue { get; init; }

	public virtual IEnumerable<Issue> Issues 
		=> _childIssues;

	public void AddChildIssue(Issue issue)
		=> _childIssues.Add(issue);

	public void Commit(Guid issueId, Guid sprintId) 
		=> RaiseDomainEvent(new CommitIssueDomainEvent(issueId, sprintId));
}