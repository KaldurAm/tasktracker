using Learn.Ddd.TaskTracker.Application.Interfaces.Providers;

namespace Learn.Ddd.TaskTracker.Api.Providers;

public class CorrelationProvider : ICorrelationProvider
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	
	public CorrelationProvider(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	/// <inheritdoc />
	public string GetCorrelationId()
	{
		return _httpContextAccessor.HttpContext?.Request.Headers["CorrelationId"].ToString() ?? string.Empty;
	}
}