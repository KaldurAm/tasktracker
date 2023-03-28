using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Events;

public record CreateIssueDomainEventHandler : INotificationHandler<CreateIssueDomainEvent>
{
	private readonly IServiceProvider _serviceProvider;

	public CreateIssueDomainEventHandler(IServiceProvider serviceProvider) 
		=> _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

	/// <inheritdoc />
	public async Task Handle(CreateIssueDomainEvent notification, CancellationToken cancellationToken)
	{
		await using var scope = _serviceProvider.CreateAsyncScope();

		var context = scope.ServiceProvider.GetRequiredService<IDataContext>();
		
		var logger = scope.ServiceProvider.GetRequiredService<ILogger<CreateIssueDomainEventHandler>>();

		var backlog = await context.Backlogs
			.FirstOrDefaultAsync(backlog => backlog.Id == notification.Issue.BacklogId, cancellationToken);

		if (backlog is null)
		{
			logger.LogWarning("Not found backlog by id {BacklogId}", notification.Issue.BacklogId);

			return;
		}

		await context.Issues.AddAsync(notification.Issue, cancellationToken);
		
		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Issue {IssueTitle} added to backlog {BacklogName}", notification.Issue.Title, backlog.Name);
	}
}