using System.ComponentModel.DataAnnotations.Schema;
using Learn.Ddd.TaskTracker.Domain.Primitives;

namespace Learn.Ddd.TaskTracker.Domain.Entities.Teams;

public class ProductOwner : AuditableEntity
{
	/// <inheritdoc />
	public ProductOwner(Guid id, string firstName, string lastName, string email) : base(id)
	{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
	}

	public string FirstName { get; init; }
	public string LastName { get; init; }
	public string Email { get; init; }

	[NotMapped]
	public string Alias => FirstName.First() + "." + LastName;
}