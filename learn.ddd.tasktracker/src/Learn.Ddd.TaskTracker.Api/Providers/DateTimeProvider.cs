using Learn.Ddd.TaskTracker.Application.Interfaces.Providers;

namespace Learn.Ddd.TaskTracker.Api.Providers;

public class DateTimeProvider : IDateTimeProvider
{
	public DateTime GetCurrentDateTime() => DateTime.UtcNow;
}