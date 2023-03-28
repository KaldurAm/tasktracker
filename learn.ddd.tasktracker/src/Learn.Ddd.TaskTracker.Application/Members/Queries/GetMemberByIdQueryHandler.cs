using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Members.Queries;

public record GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Result<TeamMember>>
{
	private readonly ILogger<GetMemberByIdQueryHandler> _logger;
	private readonly IMemberRepository _memberRepository;

	public GetMemberByIdQueryHandler(ILogger<GetMemberByIdQueryHandler> logger, IMemberRepository memberRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
	}

	/// <inheritdoc />
	public async Task<Result<TeamMember>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
	{
		var member = await _memberRepository.GetTeamMemberByIdAsync(request.Id, cancellationToken);

		if (member is null)
		{
			return Result.Failure<TeamMember>(new Error("NOT_FOUND", "Member not found by id"));
		}

		return Result.Success(member);
	}
}