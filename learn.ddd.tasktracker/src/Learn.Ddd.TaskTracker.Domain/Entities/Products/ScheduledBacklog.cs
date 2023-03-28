using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class ScheduledBacklog : AuditableEntity
{
	public Guid BacklogId { get; init; }
	public Guid ReleaseId { get; init; }
	
	public virtual Increment Increment { get; private set; } = null!;

	/// <inheritdoc />
	public ScheduledBacklog(Guid id, Guid backlogId, Guid releaseId) : base(id)
	{
		BacklogId = backlogId;
		ReleaseId = releaseId;
	}

	public void AddToRelease(Increment increment) => Increment = increment;
}