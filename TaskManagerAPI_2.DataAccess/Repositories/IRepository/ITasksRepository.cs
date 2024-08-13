using TaskManagerAPI_2.Models;

namespace TaskManagerAPI_2.Repository.IRepository
{
    public interface ITasksRepository : IRepository<TaskModel>
    {
        Task<List<TaskModel>> GetByTitleAsync(string title);
    }
}
