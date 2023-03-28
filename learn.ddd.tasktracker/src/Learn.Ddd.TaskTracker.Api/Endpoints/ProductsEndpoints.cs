using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Application.Products.Commands;
using Learn.Ddd.TaskTracker.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class ProductsEndpoints
{
	public static WebApplication AddProductsEndpoints(this WebApplication app)
	{
		app.MapGet("api/products",
			async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductsQuery(pageNumber, pageSize));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<IEnumerable<ProductResponse>>(result.Value));
			});

		app.MapGet("api/products/{productId}",
			async (Guid productId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductQuery(productId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<ProductResponse>(result.Value));
			});

		app.MapGet("api/products/details/{productId}",
			async (Guid productId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductDetailsQuery(productId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<ProductWithDetails>(result.Value));
			});

		app.MapGet("api/products/with-team",
			async (ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductsWithTeamQuery());

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<IEnumerable<ProductWithTeamResponse>>(result.Value));
			});

		app.MapGet("api/products/with-backlog",
			async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductsWithBacklogQuery(pageNumber, pageSize));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<IEnumerable<ProductWithBacklogResponse>>(result.Value));
			});

		app.MapGet("api/products/with-team-and-backlog",
			async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductsWithTeamAndBacklogQuery(pageNumber, pageSize));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<IEnumerable<ProductWithTeamAndBacklogResponse>>(result.Value));
			});

		app.MapPost("api/products/create",
			async ([FromBody] CreateProductRequest request, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new CreateProductCommand(request.Name, request.Description));

				if (result.IsFailed)
					return Results.Problem(title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Created("api/products/" + result.Value.Id, mapper.Map<ProductResponse>(result.Value));
			});

		return app;
	}
}