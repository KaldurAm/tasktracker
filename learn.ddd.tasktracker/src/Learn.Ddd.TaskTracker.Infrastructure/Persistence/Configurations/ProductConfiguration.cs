using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

internal sealed class ProductConfiguration : AuditableEntityTypeConfiguration<Product>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<Product> builder)
	{
		builder.HasIndex(i => i.Name).IsUnique();
		builder.Property(p => p.Description).HasMaxLength(500).IsRequired(false);
		builder.HasOne(o => o.Team).WithOne(o => o.Product).HasForeignKey<Team>(f => f.ProductId);
		builder.HasOne(o => o.Backlog).WithOne(o => o.Product).HasForeignKey<Backlog>(f => f.ProductId);
	}
}