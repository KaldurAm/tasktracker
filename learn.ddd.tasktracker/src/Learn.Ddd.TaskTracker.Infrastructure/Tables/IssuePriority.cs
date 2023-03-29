using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Infrastructure.Tables;

public class IssuePriority : BaseEntity<int>
{
	/// <inheritdoc />
	public IssuePriority(int id, string title) : base(id)
	{
		Title = title;
	}

	public string Title { get; init; }
}