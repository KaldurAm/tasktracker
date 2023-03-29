namespace Learn.Ddd.TaskTracker.Domain.Primitives;

public abstract class BaseEntity<TKey>
	where TKey : struct
{
	protected BaseEntity(TKey id) => Id = id;

	public TKey Id { get; set; }
}