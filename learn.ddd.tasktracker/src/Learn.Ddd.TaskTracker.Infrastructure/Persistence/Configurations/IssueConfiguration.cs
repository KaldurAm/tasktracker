using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class IssueConfiguration : AuditableEntityTypeConfiguration<Issue>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<Issue> builder)
	{
		builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
		builder.Property(p => p.Description).HasMaxLength(1000).IsRequired(false);
		builder.Property(p => p.TypeId).IsRequired();
		builder.Property(p => p.StateId).IsRequired();
		builder.Property(p => p.PriorityId).IsRequired();
		builder.HasOne(o => o.Backlog).WithMany(m => m.Issues).HasForeignKey(f => f.BacklogId).OnDelete(DeleteBehavior.NoAction);
		builder.HasOne(o => o.LinkedIssue).WithMany(m => m.Issues).HasForeignKey(f => f.LinkedIssueId).OnDelete(DeleteBehavior.SetNull);
	}
}