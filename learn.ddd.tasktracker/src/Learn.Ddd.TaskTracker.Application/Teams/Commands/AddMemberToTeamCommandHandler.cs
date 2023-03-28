using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Teams.Commands;

public record AddMemberToTeamCommandHandler : IRequestHandler<AddMemberToTeamCommand, Result>
{
	private readonly ILogger<AddMemberToTeamCommandHandler> _logger;
	private readonly IMemberRepository _memberRepository;
	private readonly IUnitOfWork _ofWork;
	private readonly ITeamRepository _teamRepository;

	public AddMemberToTeamCommandHandler(ILogger<AddMemberToTeamCommandHandler> logger,
		ITeamRepository teamRepository,
		IMemberRepository memberRepository,
		IUnitOfWork ofWork)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
		_memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
		_ofWork = ofWork ?? throw new ArgumentNullException(nameof(ofWork));
	}

	/// <inheritdoc />
	public async Task<Result> Handle(AddMemberToTeamCommand request, CancellationToken cancellationToken)
	{
		var team = await _teamRepository.GetTeamByIdIncludeMembersAsync(request.TeamId, cancellationToken);

		if (team is null)
		{
			_logger.LogWarning("Team not found by id {TeamId}", request.TeamId);

			return Result.Failure<Team>(DomainError.Product.NotFoundTeam);
		}

		if (team.Members is null)
		{
			_logger.LogWarning("Team was found but there are no members");

			return Result.Failure<Team>(DomainError.Product.NotFoundTeamInProduct);
		}

		var member = await _memberRepository.GetTeamMemberByIdAsync(request.MemberId, cancellationToken);

		if (member is null)
		{
			_logger.LogWarning("Member not found by id {MemberId}", request.MemberId);

			return Result.Failure<Team>(DomainError.Product.NotFoundMember);
		}

		team.Members.Add(member);

		await _ofWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}