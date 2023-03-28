using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class IssuePriorityConfiguration : BaseEntityConfiguration<IssuePriority, int>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<IssuePriority> builder)
	{
		builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
	}
}