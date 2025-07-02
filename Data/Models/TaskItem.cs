using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public TaskState State { get; set; } = TaskState.Waiting;

        public int AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }
        public List<UserTaskAssignment> AssignmentHistory { get; set; } = new List<UserTaskAssignment>();

    }
}
