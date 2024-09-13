using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.DTOs;
using TaskManagment.Application.Services;

namespace TaskManagment.API.Controllers
{
    [ApiController]
    [Route("api")]
   // [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly ILogger<AuthController> _logger;
        public TaskController(TaskService taskService, ILogger<AuthController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

       // [Authorize(Roles = "User,Support")]
        [HttpGet("tasks")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Test Logger");
                var result = await _taskService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred,");
            }
        }

       // [Authorize(Roles = "User,Support")]
        [HttpGet("task/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var taskDto = await _taskService.GetByIdAsync(id);
                if (taskDto == null)
                {
                    return NotFound();
                }
                return Ok(taskDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }

       // [Authorize(Roles = "User")]
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync([FromBody] TaskDto taskDto)
        {
            try
            {
                if (taskDto == null)
                {
                    return BadRequest();
                }
                _taskService.AddAsync(taskDto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = taskDto.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize(Roles = "Support")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaskDto taskDto)
        {
            try
            {
                if (taskDto == null)
                {
                    return BadRequest();
                }
                _taskService.UpdateAsync(taskDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                _taskService.DeleteAsync(id);
                return NoContent();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}
