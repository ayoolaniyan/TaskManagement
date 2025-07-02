using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Data.ViewModels;
using TaskManagement.Models;

namespace TaskManagement.Data.Services
{
    public class TaskItemService
    {
        private AppDbContext _context;

        public TaskItemService(AppDbContext context)
        {
            _context = context;
        }

        public void AddTaskItem(TaskItemVM taskItem)
        {
            var _taskItem = new TaskItem()
            {
                Title = taskItem.Title,
                State = taskItem.State
            };

            AssignUserToTask(_taskItem);

            _context.TaskItems.Add(_taskItem);
            _context.SaveChanges();
        }
        public List<TaskItem> GetTasks() => _context.TaskItems
            .Include(t => t.AssignmentHistory)
            .ToList();

        private User GetUser() => _context.Users.FirstOrDefault() ?? throw new InvalidOperationException("No user found.");
        private void AssignUserToTask(TaskItem taskItem)
        {
            var user = GetUser();
            if (user != null)
            {
                taskItem.State = TaskState.InProgress;
                taskItem.AssignedUserId = user.Id;
                taskItem.AssignedUser = user;
                taskItem.AssignmentHistory.Add(new UserTaskAssignment { User = user });
            }
        }

    }
}
