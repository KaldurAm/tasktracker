using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class IssueTypeConfiguration : BaseEntityConfiguration<IssueType, int>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<IssueType> builder)
	{
		builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
	}
}