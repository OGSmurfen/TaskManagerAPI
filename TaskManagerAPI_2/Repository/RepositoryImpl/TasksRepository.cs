using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Data;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;

namespace TaskManagerAPI_2.Repository.RepositoryImpl
{
    public class TasksRepository : RepositoryImpl<TaskModel>, ITasksRepository
    {
        public TasksRepository(TaskDbContext context) : base(context) { }
        
        public async Task<List<TaskModel>> GetByTitleAsync(string title)
        {
            return await GetAllAsync(task => task.Title == title, tracked: true); // not the most beautiful but still...

        }
    }
}
