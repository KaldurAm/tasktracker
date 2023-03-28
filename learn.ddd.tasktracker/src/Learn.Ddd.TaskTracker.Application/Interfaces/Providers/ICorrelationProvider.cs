namespace Learn.Ddd.TaskTracker.Application.Interfaces.Providers;

public interface ICorrelationProvider
{
	string GetCorrelationId();
}