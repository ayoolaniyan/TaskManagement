using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Data.Services;
using TaskManagement.Data.ViewModels;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskItemService _taskItemService;

        public TaskController(TaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpPost("create-task")]
        public IActionResult CreateTask([FromBody] TaskItemVM taskItem)
        {
            _taskItemService.AddTaskItem(taskItem);
            return Ok();
        }

        [HttpGet("get-tasks")]
        public IActionResult GetTasks()
        {
            var tasks = _taskItemService.GetTasks();
            return Ok(tasks);
        }
    }
}
