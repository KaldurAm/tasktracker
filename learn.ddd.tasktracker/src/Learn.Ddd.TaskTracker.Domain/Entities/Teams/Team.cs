using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Teams;

public class Team : AggregateRoot
{
	private readonly HashSet<TeamMember> _members = new();

	private Team(Guid id, Guid productId, string name) : base(id)
		=> (ProductId, Name) = (productId, name);

	public static Team Create(Guid id, Guid productId, string name)
		=> new(id, productId, name);

	public string Name { get; init; }

	public Guid ProductId { get; init; }
	
	public virtual Product? Product { get; init; }

	public ICollection<TeamMember> Members => _members;

	public void AddMember(Guid memberId)
	{
		RaiseDomainEvent(new AddMemberToTeamDomainEvent(Id, memberId));
	}
}