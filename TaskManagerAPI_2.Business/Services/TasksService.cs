using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI_2.Business.Services.IServices;
using TaskManagerAPI_2.DataAccess.Utilities;
using TaskManagerAPI_2.DTOs;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;

namespace TaskManagerAPI_2.Business.Services
{
    public class TasksService : ITasksService
    {

        private readonly ITasksRepository tasksRepository;

        public TasksService(ITasksRepository tasksRepository)
        {
            this.tasksRepository = tasksRepository;
        }





        public async Task<List<TaskDTO>> GetAllTasksAsync()
        {
            var tasks = await tasksRepository.GetAllAsync();
            return tasks.Select(TaskToDTO).ToList();
        }
        public async Task<List<TaskDTO>> GetAllTasksFilteredPaged
            (
                int? IdBeginning,
                int? IdEnd,
                string? Title,
                string? Description,
                string? Status,
                string? Priority,
                DateOnly? DueDate,
                int? pageNumber,
                int? pageSize
            )
        {
            var tasks = await tasksRepository.GetTasksFilteredPaged(IdBeginning, IdEnd, Title,
                                    Description, Status, Priority, DueDate, pageNumber, pageSize);
            return tasks.Select(TaskToDTO).ToList();

        }
        //public async List<TaskModel> GetAllTasksFilteredPaged(TaskQueryBuilder? tasksFilter, int pageNumber = 1, int pageSize = 15)
        //{
        //    List<TaskDTO> tasks;

        //    if (filterTask != null)
        //    {
        //        tasks = 
        //    }


        //    return 
        //}








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
