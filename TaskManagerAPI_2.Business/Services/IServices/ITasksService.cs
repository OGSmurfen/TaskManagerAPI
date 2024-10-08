﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.DataAccess.Utilities;
using TaskManagerAPI_2.DTOs;
using TaskManagerAPI_2.Models;

namespace TaskManagerAPI_2.Business.Services.IServices
{
    public interface ITasksService
    {
        Task<List<TaskDTO>> GetAllTasksAsync();
        Task<TaskModel> GetTaskByIdAsync(int taskId);
        Task<List<TaskDTO>> GetAllTasksFilteredPaged(
                int? IdBeginning,
                int? IdEnd,
                string? Title,
                string? Description,
                string? Status,
                string? Priority,
                DateOnly? DueDate,
                int? pageNumber,
                int? pageSize);
        Task<bool> DeleteTaskById(int id);

        Task<bool> CreateTask(TaskModel task);
        Task<bool> UpdateTask(TaskModel task);

    }
}
