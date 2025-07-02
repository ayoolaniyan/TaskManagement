using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class UserTaskAssignment
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public TaskItem Task { get; set; } = null;
        public int UserId { get; set; }
        public User User { get; set; } = null;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    }
}
