using Microsoft.EntityFrameworkCore;
using TaskManagerAPI_2.Models;
using TaskManagerAPI_2.Repository.IRepository;

namespace TaskManagerAPI_2.Repository.RepositoryImpl
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext _context;
        protected readonly DbSet<T> _tasks;


        public Repository(DbContext context)
        {
            _context = context;
            _tasks = context.Set<T>();
        }


        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
