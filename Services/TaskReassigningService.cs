using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class TaskReassigningService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public TaskReassigningService(IServiceScopeFactory scopeFactory) =>
            _scopeFactory = scopeFactory;

        private readonly Random _random = new Random();

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var tasks = dbContext.TaskItems
                    .Include(t => t.AssignedUser)
                    .Include(t => t.AssignmentHistory)
                    .ToList();

                var allUsers = dbContext.Users.ToList();

                foreach (var task in tasks.Where(t => t.State != TaskState.Completed))
                {
                    var history = task.AssignmentHistory
                        .OrderByDescending(h => h.AssignedAt)
                        .ToList();

                    var currentUserId = task.AssignedUserId;
                    var previousUserId = history.Skip(1).FirstOrDefault()?.UserId;

                    var assignedUserIds = task.AssignmentHistory
                        .Select(h => h.UserId)
                        .Distinct()
                        .ToHashSet();

                    var allUserIds = allUsers.Select(u => u.Id).ToHashSet();
                    var missingUserIds = allUserIds.Except(assignedUserIds).ToList();

                    if (!missingUserIds.Any())
                    {
                        task.State = TaskState.Completed;
                        task.AssignedUser = null;
                        continue;
                    }

                    var prospectUsers = allUsers
                        .Where(u => u.Id != currentUserId && u.Id != previousUserId)
                        .Where(u => !assignedUserIds.Contains(u.Id) || missingUserIds.Contains(u.Id))
                        .ToList();

                    if (!prospectUsers.Any())
                    {
                        prospectUsers = allUsers
                            .Where(u => u.Id != currentUserId)
                            .Where(u => !assignedUserIds.Contains(u.Id) || missingUserIds.Contains(u.Id))
                            .ToList();
                    }

                    if (!prospectUsers.Any())
                    {
                        task.AssignedUser = null;
                        task.State = TaskState.Waiting;
                        continue;
                    }

                    var newUser = prospectUsers[_random.Next(prospectUsers.Count)];

                    task.AssignedUser = newUser;
                    task.AssignedUserId = newUser.Id;
                    task.State = TaskState.InProgress;

                    task.AssignmentHistory.Add(new UserTaskAssignment
                    {
                        TaskId = task.Id,
                        UserId = newUser.Id,
                        AssignedAt = DateTime.UtcNow
                    });
                }

                await dbContext.SaveChangesAsync();
                await Task.Delay(TimeSpan.FromMinutes(2), cancellationToken);
            }
        }
    }
}
