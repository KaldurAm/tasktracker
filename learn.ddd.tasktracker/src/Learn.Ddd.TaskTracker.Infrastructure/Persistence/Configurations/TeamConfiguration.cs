using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class TeamConfiguration : AuditableEntityTypeConfiguration<Team>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<Team> builder)
	{
		builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
		builder.HasMany(m => m.Members).WithOne().HasForeignKey(f => f.TeamId).OnDelete(DeleteBehavior.SetNull);
	}
}