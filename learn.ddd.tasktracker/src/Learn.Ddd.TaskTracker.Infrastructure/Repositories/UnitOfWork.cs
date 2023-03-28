using Learn.Ddd.TaskTracker.Application.Interfaces.Providers;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Primitives;
using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly DataContext _context;
	private readonly IDateTimeProvider _dateTimeProvider;
	private readonly IUserProvider _userProvider;

	public UnitOfWork(DataContext context, IDateTimeProvider dateTimeProvider, IUserProvider userProvider)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
		_userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		ConvertDomainEventsToOutboxMessages();
		
		BeforeSaveChanges();
		
		await _context.SaveChangesAsync(cancellationToken);
	}
	
	private void ConvertDomainEventsToOutboxMessages()
	{
		var outboxMessages = _context.ChangeTracker
			.Entries<AggregateRoot>()
			.Select(x => x.Entity)
			.SelectMany(aggregateRoot =>
			{
				var domainEvents = aggregateRoot.GetDomainEvents();

				aggregateRoot.ClearDomainEvents();

				return domainEvents;
			})
			.Select(domainEvent => new OutboxMessage
			{
				Id = Guid.NewGuid(),
				OccurredOnUtc = DateTime.UtcNow,
				Type = domainEvent.GetType().Name,
				Content = JsonConvert.SerializeObject(
					domainEvent,
					new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, }),
			})
			.ToList();

		_context.Set<OutboxMessage>().AddRange(outboxMessages);
	}
	
	private void BeforeSaveChanges()
	{
		var entries = _context.ChangeTracker
			.Entries<AuditableEntity>()
			.Where(x => x.State is EntityState.Added or EntityState.Modified);

		foreach(var entry in entries)
		{
			switch(entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedAt = _dateTimeProvider.GetCurrentDateTime();
					entry.Entity.CreatedBy = _userProvider.GetCurrentUserId();

					break;
				case EntityState.Modified:
					entry.Entity.ModifiedAt = _dateTimeProvider.GetCurrentDateTime();
					entry.Entity.ModifiedBy = _userProvider.GetCurrentUserId();

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}