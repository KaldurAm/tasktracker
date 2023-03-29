using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class Backlog : AggregateRoot
{
	private readonly HashSet<Issue> _issues = new();

	private Backlog(Guid id, Guid productId, string name) : base(id)
	{
		(ProductId, Name) = (productId, name);
	}

	public Guid ProductId { get; init; }

	public string Name { get; init; }

	public virtual Product? Product { get; init; }

	public virtual IEnumerable<Issue> Issues
		=> _issues;

	public static Backlog Create(Guid id, Guid productId, string name)
	{
		return new(id, productId, name);
	}

	public void CreateIssue(Issue issue)
	{
		RaiseDomainEvent(new CreateIssueDomainEvent(issue));
	}

	public void AddIssue(Issue issue)
	{
		_issues.Add(issue ?? throw new ArgumentNullException(nameof(issue)));
	}
}