using TaskManagerAPI_2.DataAccess.Utilities;
using TaskManagerAPI_2.Models;

namespace TaskManagerAPI_2.Repository.IRepository
{
    public interface ITasksRepository : IRepository<TaskModel>
    {
        Task<List<TaskModel>> GetByTitleAsync(string title);
        Task<List<TaskModel>> GetTasksFilteredPaged
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
            );

    }
}
