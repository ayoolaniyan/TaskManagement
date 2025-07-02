using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement.Models
{
    public class User
    {
        public int Id { set; get; }
        public string Name { set; get; } = string.Empty;
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
