namespace Learn.Ddd.TaskTracker.Application.Persistence;

public interface IUnitOfWork
{
	Task SaveChangesAsync(CancellationToken cancellationToken = default);
}