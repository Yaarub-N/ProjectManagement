using Business.Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController(TaskService taskService) : ControllerBase
    {
        private readonly TaskService _taskService = taskService;

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskDTO)
        {
            var response = await _taskService.CreateTaskAsync(taskDTO);
            return response.Success ? Created("", response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTask(int taskId)
        {
            var response = await _taskService.GetTaskByIdAsync(taskId);
            return response.Success ? Ok(response.Data) : NotFound(response.Message);
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskDTO taskDTO)
        {
            var response = await _taskService.UpdateTaskAsync(taskId, taskDTO);
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var response = await _taskService.DeleteTaskAsync(taskId);
            return response.Success ? NoContent() : BadRequest(response.Message);
        }
    }
}
