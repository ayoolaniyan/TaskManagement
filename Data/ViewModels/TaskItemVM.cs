using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.Data.ViewModels
{
    public class TaskItemVM
    {
        public required string Title { get; set; }
        public TaskState State { get; set; } = TaskState.Waiting;

    }
}
