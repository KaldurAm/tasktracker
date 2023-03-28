using Learn.Ddd.TaskTracker.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public abstract class BaseEntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
	where TEntity : BaseEntity<TKey>
	where TKey : struct
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(k => k.Id);
	}
	
	public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
}