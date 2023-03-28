using System.Security.Claims;
using Learn.Ddd.TaskTracker.Application.Interfaces.Providers;

namespace Learn.Ddd.TaskTracker.Api.Providers;

public class UserProvider : IUserProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public UserProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	/// <inheritdoc />
	public string GetCurrentUserId()
	{
		return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
	}
}