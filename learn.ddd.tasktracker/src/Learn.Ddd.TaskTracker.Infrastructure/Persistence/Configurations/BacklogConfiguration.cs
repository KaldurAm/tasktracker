using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

internal sealed class BacklogConfiguration : AuditableEntityTypeConfiguration<Backlog>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<Backlog> builder)
	{
		builder.Property(p => p.ProductId).IsRequired();
		builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
		builder.HasMany(m => m.Issues).WithOne().HasForeignKey(f => f.BacklogId).OnDelete(DeleteBehavior.Cascade);
	}
}