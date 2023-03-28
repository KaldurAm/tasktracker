using System.Reflection;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.EntityFrameworkCore;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
	/// <inheritdoc />
	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	/// <inheritdoc />
	public DbSet<Product> Products { get; set; } = null!;
	
	/// <inheritdoc />
	public DbSet<Team> Teams { get; set; } = null!;

	/// <inheritdoc />
	public DbSet<TeamMember> TeamMembers { get; set; }

	/// <inheritdoc />
	public DbSet<ProductOwner> ProductOwners { get; set; }

	/// <inheritdoc />
	public DbSet<Sprint> Sprints { get; set; } = null!;
	
	/// <inheritdoc />
	public DbSet<Backlog> Backlogs { get; set; } = null!;

	/// <inheritdoc />
	public DbSet<Issue> Issues { get; set; } = null!;
	
	/// <inheritdoc />
	public DbSet<IssuePriority> IssuePriorities { get; set; } = null!;

	/// <inheritdoc />
	public DbSet<IssueType> IssueTypes { get; set; } = null!;

	/// <inheritdoc />
	public DbSet<IssueState> IssueStates { get; set; } = null!;

	/// <inheritdoc />
	public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
	
	/// <inheritdoc />
	public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; } = null!;
	
	/// <inheritdoc />
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}