using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class IssuePriority : BaseEntity<int>
{
	/// <inheritdoc />
	public IssuePriority(int id, string title) : base(id)
		=> Title = title ?? throw new ArgumentNullException(nameof(title));

	public string Title { get; init; } = string.Empty;
}