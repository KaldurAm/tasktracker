using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class ProductOwnerConfiguration : AuditableEntityTypeConfiguration<ProductOwner>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<ProductOwner> builder)
	{
		builder.Property(p => p.Email).IsRequired();
		builder.Property(p => p.FirstName).IsRequired();
		builder.Property(p => p.LastName).IsRequired();
	}
}