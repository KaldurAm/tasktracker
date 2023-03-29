using Learn.Ddd.TaskTracker.Domain.Enums;
using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Learn.Ddd.TaskTracker.Infrastructure.Persistence.Initializers;

public static class Seeder
{
	public static async Task StartAsync(IApplicationBuilder app)
	{
		var scope = app.ApplicationServices.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<DataContext>();

		//await context.Database.EnsureDeletedAsync();

		//await context.Database.EnsureCreatedAsync();

		var issuePrioritiesCreated = await context.IssuePriorities.AnyAsync();

		var issueStatesCreated = await context.IssueStates.AnyAsync();

		var issueTypesCreated = await context.IssueTypes.AnyAsync();

		if (!issuePrioritiesCreated)
		{
			var priorities = new HashSet<IssuePriority>
			{
				new((int)IssuePriorityEnum.MINOR, "Minor"),
				new((int)IssuePriorityEnum.NORMAL, "Normal"),
				new((int)IssuePriorityEnum.MAJOR, "Major"),
				new((int)IssuePriorityEnum.CRITICAL, "Critical"),
				new((int)IssuePriorityEnum.STOPPER, "Show stopper"),
			};

			await context.IssuePriorities.AddRangeAsync(priorities);
		}

		if (!issueStatesCreated)
		{
			var stateList = new HashSet<IssueState>
			{
				new((int)IssueStateEnum.BACKLOG, "Backlog"),
				new((int)IssueStateEnum.OPEN, "Open"),
				new((int)IssueStateEnum.IN_PROGRESS, "In progress"),
				new((int)IssueStateEnum.ON_MERGE, "On merge"),
				new((int)IssueStateEnum.ON_REVIEW, "On review"),
				new((int)IssueStateEnum.ON_TEST, "On test"),
				new((int)IssueStateEnum.DONE, "Done"),
			};

			await context.IssueStates.AddRangeAsync(stateList);
		}

		if (!issueTypesCreated)
		{
			HashSet<IssueType> issueTypes = new()
			{
				new IssueType((int)IssueTypeEnum.EPIC, "Epic"),
				new IssueType((int)IssueTypeEnum.USER_STORY, "User story"),
				new IssueType((int)IssueTypeEnum.TASK, "Task"),
				new IssueType((int)IssueTypeEnum.FIX, "Fix"),
			};

			await context.IssueTypes.AddRangeAsync(issueTypes);
		}

		try
		{
			if (!issueStatesCreated || !issueTypesCreated || !issuePrioritiesCreated)
				await context.SaveChangesAsync();
		}
		catch(Exception ex)
		{
			await Console.Error.WriteLineAsync(ex.Message);

			throw new InvalidOperationException("Error occured while saving changes", ex);
		}
	}
}