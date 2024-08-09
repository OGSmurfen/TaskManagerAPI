using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Repository.IRepository;
using TaskManagerAPI.Repository.RepositoryImpl;

namespace TaskManagerAPI.UnitOfWork
{
    public class UnitOfWorkImpl : IUnitOfWork
    {
        private readonly DbContext _context;

        public IRepository<TaskModel> Tasks { get; private set; }


        public UnitOfWorkImpl(DbContext context)
        {
            _context = context;
            Tasks = new TaskRepositoryImpl(context);
        }




        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
