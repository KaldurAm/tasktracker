using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;

namespace Learn.Ddd.TaskTracker.Domain.Primitives;

public abstract class AggregateRoot : AuditableEntity
{
	private readonly HashSet<IDomainEvent> _domainEvents = new();

	protected AggregateRoot(Guid id) : base(id)
	{
		CreatedAt = DateTime.UtcNow;
		CreatedBy = null;
		ModifiedAt = null;
		ModifiedBy = null;
	}

	public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
	{
		return _domainEvents.ToList();
	}

	public void ClearDomainEvents()
	{
		_domainEvents.Clear();
	}

	protected void RaiseDomainEvent(IDomainEvent @event)
	{
		_domainEvents.Add(@event);
	}
}