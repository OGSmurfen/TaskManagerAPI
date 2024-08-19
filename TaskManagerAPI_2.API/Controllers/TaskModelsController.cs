using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Business.Services.IServices;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.DTOs;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;


namespace TaskManagerAPI_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskModelsController : ControllerBase
    {


        private readonly ITasksService tasksService;
        public TaskModelsController(ITasksService tasksService)
        {
            this.tasksService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks(
               [FromQuery] int? IdBeginning,
               [FromQuery] int? IdEnd,
               [FromQuery] string? Title,
               [FromQuery] string? Description,
               [FromQuery] string? Status,
               [FromQuery] string? Priority,
               [FromQuery] DateOnly? DueDate,
               [FromQuery] int? pageNumber,
               [FromQuery] int? pageSize
            )
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and/or page size cannot be 0 or less");
            }

            var tasks = await tasksService.GetAllTasksFilteredPaged(
                IdBeginning, IdEnd, Title, Description, Status, Priority, DueDate, pageNumber, pageSize
                );



             return Ok(tasks);

             
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            bool result = await tasksService.DeleteTaskById(id);

            if(result)
            {
                return NoContent(); // 204
            }
            else
            {
                return NotFound(); // 404
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> PostTask(TaskDTO taskDTO)
        {
            if (taskDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }

            var taskModel = new TaskModel
            {
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                Status = taskDTO.Status,
                Priority = taskDTO.Priority,
                DueDate = taskDTO.DueDate,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var success = await tasksService.CreateTask(taskModel);
            if (success)
            {
                return CreatedAtAction(nameof(PostTask), new { id = taskModel.Id }, taskModel); // 201 Created
            }
            else
            {
                return StatusCode(500, "An error occurred while creating the task."); // 500 Internal Server Error
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskDTO taskDTO)
        {
            if(id != taskDTO.Id)
            {
                return BadRequest();
            }


            var taskModel = await tasksService.GetTaskByIdAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            taskModel.Title = taskDTO.Title;
            taskModel.Description = taskDTO.Description;
            taskModel.Status = taskDTO.Status;
            taskModel.Priority = taskDTO.Priority;
            taskModel.UpdatedAt = DateTime.UtcNow; // because we are currently updating it
            taskModel.DueDate = taskDTO.DueDate;

            try
            {
                await tasksService.UpdateTask(taskModel);
            }catch (DBConcurrencyException)
            {
                if (!TaskModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }


            return NoContent();
        }






        private static TaskDTO TaskToDTO(TaskModel x) =>
            new TaskDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                Priority = x.Priority,
                DueDate = x.DueDate
            };
        private bool TaskModelExists(int id)
        {
            var task = tasksService.GetTaskByIdAsync(id);
            if(task != null)
            {
                return true;
            }
            return false;
        }
    }
}
