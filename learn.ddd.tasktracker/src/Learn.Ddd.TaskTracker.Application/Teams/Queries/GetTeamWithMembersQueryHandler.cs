using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Teams.Queries;

public record GetTeamWithMembersQueryHandler : IRequestHandler<GetTeamWithMembersQuery, Result<Team>>
{
	private readonly ILogger<GetTeamWithMembersQueryHandler> _logger;
	private readonly ITeamRepository _teamRepository;

	public GetTeamWithMembersQueryHandler(ILogger<GetTeamWithMembersQueryHandler> logger, ITeamRepository teamRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
	}

	/// <inheritdoc />
	public async Task<Result<Team>> Handle(GetTeamWithMembersQuery request, CancellationToken cancellationToken)
	{
		var team = await _teamRepository.GetTeamByIdIncludeMembersAsync(request.TeamId, cancellationToken);

		if (team is null)
			return Result.Failure<Team>(DomainError.Product.NotFoundTeam);

		return Result.Success(team);
	}
}