using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<OutboxMessage> builder)
	{
		builder.ToTable("OutboxMessages");
		builder.HasKey(k => k.Id);
	}
}