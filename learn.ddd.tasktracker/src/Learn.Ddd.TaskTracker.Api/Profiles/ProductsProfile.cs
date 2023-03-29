using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;

namespace Learn.Ddd.TaskTracker.Api.Profiles;

public class ProductsProfile : Profile
{
	public ProductsProfile()
	{
		CreateMap<Product, ProductResponse>();
		CreateMap<Product, ProductWithTeamResponse>();
		CreateMap<Product, ProductWithBacklogResponse>();
		CreateMap<Product, ProductWithTeamAndBacklogResponse>();
		CreateMap<Product, ProductWithDetails>();

		CreateMap<Backlog, BacklogResponse>();
		CreateMap<Backlog, BacklogWithIssuesResponse>();

		CreateMap<Issue, IssueResponse>();
	}
}