using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.DataAccess.Utilities;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;

namespace TaskManagerAPI_2.Repository.RepositoryImpl
{
    public class TasksRepository : RepositoryImpl<TaskModel>, ITasksRepository
    {
        public TasksRepository(TaskDbContext context) : base(context) { }
        
        public async Task<List<TaskModel>> GetByTitleAsync(string title)
        {
            return await GetAllAsync(task => task.Title == title, tracked: true); 

        }

        public async Task<List<TaskModel>> GetTasksFilteredPaged
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
            IQueryable<TaskModel> query = dbSet;

            
            query = TaskQueryBuilder.ApplyFilters(query, IdBeginning, IdEnd, Title,
                                        Description, Status, Priority, DueDate);

            query = TaskQueryBuilder.ApplyPaging(query, pageNumber, pageSize);


            return await query.ToListAsync();
        }

    }
}
