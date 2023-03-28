using KDS.Primitives.FluentResult;

namespace Learn.Ddd.TaskTracker.Domain.Errors;

public static class DomainError
{
	public static class Product
	{
		private const string _notFoundCode = "NOT_FOUND";

		public static Error NotFoundProducts => new(_notFoundCode, "There are no products in database");
		public static Error NotFoundProduct => new(_notFoundCode, "There is no product matching an id");
		public static Error NotFoundBacklog => new(_notFoundCode, "There is no backlog in product");
		public static Error NotFoundTeam => new(_notFoundCode, "There is no team in product");
		public static Error NotFoundTeamInProduct => new(_notFoundCode, "Product was found but there is no team");
		public static Error NotFoundIssues => new(_notFoundCode, "There are no issues in product backlog");
		public static Error NotFoundIssue => new(_notFoundCode, "There is no issue in product backlog matching an id");
		public static Error NotFoundSprint => new(_notFoundCode, "There is no sprint in product matching an id");
		public static Error NotFoundMember => new(_notFoundCode, "There is no member mathing an id");
	}

	public static class Auth
	{
		private const string _authErrorCode = "AUTH_FAILED";

		public static Error PasswordNotConfirm => new(_authErrorCode, "Password and confirm password does not match");
		public static Error UnableToCreateUser => new(_authErrorCode, "User registration failed");
		public static Error AddToRoleFailed => new(_authErrorCode, "User could not be add to role");
		public static Error UserNotFoundByLogin => new(_authErrorCode, "User not found by login");
		public static Error InvalidLoginOrPassword => new(_authErrorCode, "Invalid login or password");
	}
}