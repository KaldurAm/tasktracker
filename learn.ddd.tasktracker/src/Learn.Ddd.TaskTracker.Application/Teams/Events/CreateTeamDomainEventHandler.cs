using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Teams.Events;

public record CreateTeamDomainEventHandler : INotificationHandler<CreateTeamDomainEvent>
{
	private readonly IServiceProvider _serviceProvider;

	public CreateTeamDomainEventHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	/// <inheritdoc />
	public async Task Handle(CreateTeamDomainEvent notification, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<IDataContext>();

		var logger = scope.ServiceProvider.GetRequiredService<ILogger<CreateTeamDomainEventHandler>>();

		var product = await context.Products.FindAsync(new object?[] { notification.ProductId, }, cancellationToken);

		if (product is null)
		{
			logger.LogWarning("Not found product by id {ProductId}", notification.ProductId);

			return;
		}

		var team = Team.Create(Guid.NewGuid(), product.Id, product.Name + " team");

		await context.Teams.AddAsync(team, cancellationToken);

		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Team {TeamName} added to product {ProductName}", team.Name, product.Name);
	}
}