using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

internal class SprintConfiguration : AuditableEntityTypeConfiguration<Sprint>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<Sprint> builder)
	{
		builder.Property(p => p.ProductId).IsRequired();
		builder.Property(p => p.Title).HasMaxLength(50).IsRequired();
		builder.Property(p => p.Start).IsRequired();
		builder.Property(p => p.Finish).IsRequired();
		builder.Property(p => p.Goal).HasMaxLength(200).IsRequired(false);
		builder.HasOne(o => o.Product).WithMany(m => m.Sprints).HasForeignKey(f => f.ProductId).OnDelete(DeleteBehavior.NoAction);
	}
}