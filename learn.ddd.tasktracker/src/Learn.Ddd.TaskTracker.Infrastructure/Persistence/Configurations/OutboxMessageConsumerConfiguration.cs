using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Configurations;

public class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
{
	/// <inheritdoc />
	public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
	{
		builder.ToTable("OutboxMessageConsumers");

		builder.HasKey(outboxMessageConsumer =>
			new { outboxMessageConsumer.Id, outboxMessageConsumer.Name, });
	}
}