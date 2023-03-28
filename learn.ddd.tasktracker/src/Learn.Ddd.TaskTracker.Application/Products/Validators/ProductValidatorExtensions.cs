using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;

namespace Learn.Ddd.TaskTracker.Application.Products.Validators;

public static class ProductValidatorExtensions
{
	public static Result<Product> ValidateAndReturnResult(this Product? product)
	{
		if (product is null)
			return Result.Failure<Product>(DomainError.Product.NotFoundProduct);

		return Result.Success(product);
	}

	public static Result<IEnumerable<Product>> ValidateAndReturnResult(this IEnumerable<Product> products)
	{
		if (!products.Any())
			return Result.Failure<IEnumerable<Product>>(DomainError.Product.NotFoundProduct);

		return Result.Success(products);
	}
}