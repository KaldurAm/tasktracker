using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;

namespace Learn.Ddd.TaskTracker.Api.Profiles;

public class TeamsProfile : Profile
{
	public TeamsProfile()
	{
		CreateMap<Team, TeamResponse>();
		CreateMap<TeamMember, MemberResponse>();
		CreateMap<Team, TeamWithMembersResponse>();
	}
}