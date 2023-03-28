using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Products;

public class Sprint : AuditableEntity
{
	private readonly HashSet<Issue> _issues = new();

	/// <inheritdoc />
	public Sprint(Guid id,
		Guid productId,
		string title,
		DateOnly start,
		DateOnly finish,
		string? goal = null) : base(id)
	{
		ProductId = productId;
		Title = title ?? throw new ArgumentNullException(nameof(title));
		Start = start;
		Finish = finish;
		Goal = goal ?? string.Empty;
	}

	public Guid ProductId { get; init; }
	public string Title { get; init; }
	public DateOnly Start { get; init; }
	public DateOnly Finish { get; init; }
	public string Goal { get; init; }

	public virtual Product Product { get; private set; } = null!;

	public virtual ICollection<Issue> Issues
		=> _issues;
}