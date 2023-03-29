using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class Product : AggregateRoot
{
	private readonly HashSet<Sprint> _sprints = new();

	private Product(Guid id, string name, string description) : base(id)
	{
		Name = name ?? throw new ArgumentNullException(nameof(name));
		Description = description ?? string.Empty;
	}

	public string Name { get; init; }
	public string Description { get; init; }

	public virtual Team? Team { get; init; }
	public virtual Backlog? Backlog { get; init; }

	public virtual ICollection<Sprint> Sprints
		=> _sprints;

	public static Product Create(Guid id, string name, string? description = default)
		=> new(id, name, description ?? string.Empty);

	public void AddTeam()
		=> RaiseDomainEvent(new CreateTeamDomainEvent(Id));

	public void AddBacklog()
		=> RaiseDomainEvent(new CreateBacklogDomainEvent(Id));

	public void AddSprint(Sprint sprint)
		=> RaiseDomainEvent(new CreateSprintDomainEvent(sprint.ProductId, sprint.Title, sprint.Goal, sprint.Start, sprint.Finish));
}