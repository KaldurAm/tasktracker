using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Infrastructure.Tables;

public class IssueState : BaseEntity<int>
{
	/// <inheritdoc />
	public IssueState(int id, string title) : base(id)
	{
		Title = title;
	}

	public string Title { get; init; }
}