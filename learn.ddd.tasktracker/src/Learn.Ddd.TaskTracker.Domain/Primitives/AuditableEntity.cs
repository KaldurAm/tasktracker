namespace Learn.Ddd.TaskTracker.Domain.Primitives;

public abstract class AuditableEntity : BaseEntity<Guid>
{
	protected AuditableEntity(Guid id) : base(id)
	{
		CreatedAt = DateTime.UtcNow;
	}

	public DateTime CreatedAt { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime? ModifiedAt { get; set; }
	public string? ModifiedBy { get; set; }
}