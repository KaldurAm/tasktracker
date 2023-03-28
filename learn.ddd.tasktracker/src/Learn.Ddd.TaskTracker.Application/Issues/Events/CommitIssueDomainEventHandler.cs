using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Events;

public class CommitIssueDomainEventHandler : INotificationHandler<CommitIssueDomainEvent>
{
	private readonly IServiceProvider _serviceProvider;

	public CommitIssueDomainEventHandler(IServiceProvider serviceProvider) 
		=> _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

	/// <inheritdoc />
	public async Task Handle(CommitIssueDomainEvent notification, CancellationToken cancellationToken)
	{
		await using var scope = _serviceProvider.CreateAsyncScope();

		var context = scope.ServiceProvider.GetRequiredService<IDataContext>();

		var logger = scope.ServiceProvider.GetRequiredService<ILogger<CommitIssueDomainEventHandler>>();

		var commitedIssue = await context.Issues
			.FirstOrDefaultAsync(issue => issue.Id == notification.IssueId, cancellationToken);

		if (commitedIssue is null)
		{
			logger.LogWarning("Not found issue by id {IssueId}", notification.IssueId);

			return;
		}

		var sprint = await context.Sprints
			.FirstOrDefaultAsync(sprint => sprint.Id == notification.SprintId, cancellationToken);

		if (sprint is null)
		{
			logger.LogWarning("Not found sprint by id {SprintId}", notification.SprintId);

			return;
		}

		sprint.Issues.Add(commitedIssue);

		await context.SaveChangesAsync(cancellationToken);
	}
}