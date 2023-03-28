using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Infrastructure.Tables;

public class IssueType : BaseEntity<int>
{
	/// <inheritdoc />
	public IssueType(int id, string title) : base(id)
		=> Title = title;
	
	public string Title { get; init; }
}