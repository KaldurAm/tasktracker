using Learn.Ddd.TaskTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public abstract class AuditableEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
	where TEntity : AuditableEntity
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(k => k.Id);
		builder.Property(p => p.CreatedAt).IsRequired();
		builder.Property(p => p.CreatedBy).IsRequired(false);
		builder.Property(p => p.ModifiedAt).IsRequired(false);
		builder.Property(p => p.ModifiedBy).IsRequired(false);
	}

	public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}