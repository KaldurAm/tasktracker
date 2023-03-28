using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Teams;

public class TeamMember : AuditableEntity
{
	/// <inheritdoc />
	private TeamMember(Guid id, Guid? teamId, string firstName, string lastName, string email) : base(id)
	{
		TeamId = teamId;
		FirstName = firstName;
		LastName = lastName;
		Email = email;
	}

	public static TeamMember Create(string firstName, string lastName, string email)
		=> new(Guid.NewGuid(), default, firstName, lastName, email);

	public Guid? TeamId { get; init; }
	public string FirstName { get; init; } = string.Empty;
	public string LastName { get; init; } = string.Empty;
	public string Email { get; init; } = string.Empty;

	public virtual Team? Team { get; init; }

	public string Alias => LastName.ToLower() + "." + FirstName.ToLower().First();
}