using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class IssueType : BaseEntity<int>
{
	public IssueType(int id, string title) : base(id) 
		=> Title = title;

	public string Title { get; init; } = string.Empty;
}