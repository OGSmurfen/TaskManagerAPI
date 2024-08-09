using TaskManagerAPI.Models;
using TaskManagerAPI.Repository.IRepository;

namespace TaskManagerAPI.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TaskModel> Tasks { get; }
        int Complete();


    }
}
