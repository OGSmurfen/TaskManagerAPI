using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.DTOs;
using TaskManagerAPI_2.Models;

namespace TaskManagerAPI_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskModelsController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TaskModelsController(TaskDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDTO>>> GetTasks()
        {
            return await _context.Tasks.Select(x => TaskToDTO(x)).ToListAsync();
        }


        // GET: api/TaskModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDTO>> GetTaskModel(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);

            if (taskModel == null)
            {
                return NotFound();
            }

            return TaskToDTO(taskModel);
        }

        // PUT: api/TaskModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskModel(int id, TaskDTO taskDTO)
        {
            if (id != taskDTO.Id)
            {
                return BadRequest();
            }

            var taskModel = await _context.Tasks.FindAsync(id);
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
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
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

        // POST: api/TaskModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskDTO>> PostTaskModel(TaskDTO taskDTO)
        {
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


            _context.Tasks.Add(taskModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskModel), new { id = taskModel.Id }, taskModel);
        }

        // DELETE: api/TaskModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskModel(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
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
    }
}
