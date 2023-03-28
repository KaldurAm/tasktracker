using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Members.Commands;

public record CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Result<Guid>>
{
	private readonly ILogger<CreateMemberCommandHandler> _logger;
	private readonly IMemberRepository _memberRepository;
	private readonly IUnitOfWork _ofWork;

	public CreateMemberCommandHandler(ILogger<CreateMemberCommandHandler> logger, IMemberRepository memberRepository, IUnitOfWork ofWork)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
		_ofWork = ofWork ?? throw new ArgumentNullException(nameof(ofWork));
	}

	/// <inheritdoc />
	public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
	{
		var newMember = TeamMember.Create(request.FirstName, request.LastName, request.Email);
		
		_logger.LogInformation("New member generated {TeamMember}", newMember);

		await _memberRepository.CreateTeamMemberAsync(newMember, cancellationToken);

		await _ofWork.SaveChangesAsync(cancellationToken);

		return Result.Success(newMember.Id);
	}
}