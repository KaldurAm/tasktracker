using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class TeamMemberConfiguration : AuditableEntityTypeConfiguration<TeamMember>
{
	/// <inheritdoc />
	public override void ConfigureEntity(EntityTypeBuilder<TeamMember> builder)
	{
		builder.Property(p => p.FirstName).HasMaxLength(150).IsRequired();
		builder.Property(p => p.LastName).HasMaxLength(150).IsRequired();
		builder.Property(p => p.Email).HasMaxLength(255).IsRequired();
		builder.HasOne(o => o.Team).WithMany(m => m.Members).HasForeignKey(f => f.TeamId).OnDelete(DeleteBehavior.NoAction);
	}
}